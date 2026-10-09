using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using kafkaRunnerWindows.Models;

namespace kafkaRunnerWindows.Services
{
    public class ProcessRunnerService : IDisposable
    {
        private Process? _runningProcess;
        private ProjectRecord? _currentProject;
        private readonly string _runnerScriptPath;
        private readonly object _lock = new();
        private CancellationTokenSource? _monitorCts;
        private bool _isReady;

        public event Action<string, bool>? OutputReceived;
        public event Action<ProjectRecord>? ProcessStarted;  // Waiting / Compiling
        public event Action<ProjectRecord>? ProcessReady;    // Fully running & active
        public event Action<ProjectRecord, int>? ProcessFinished;

        public ProcessRunnerService()
        {
            var dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kafka-runner-windows");
            Directory.CreateDirectory(dataDir);
            _runnerScriptPath = Path.Combine(dataDir, "run_active.bat");
        }

        public bool IsRunning
        {
            get
            {
                lock (_lock)
                {
                    try
                    {
                        return _runningProcess != null && !_runningProcess.HasExited;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool IsReady
        {
            get
            {
                lock (_lock)
                {
                    return _isReady && IsRunning;
                }
            }
        }

        public ProjectRecord? CurrentProject
        {
            get
            {
                lock (_lock)
                {
                    return _currentProject;
                }
            }
        }

        public async Task<bool> StartAsync(ProjectRecord project, RunnerConfig config)
        {
            if (IsRunning)
            {
                OutputReceived?.Invoke("[WARN] A process is already running. Please stop it first.", true);
                return false;
            }

            if (!Directory.Exists(project.Path))
            {
                OutputReceived?.Invoke($"[ERROR] Project directory not found: {project.Path}", true);
                return false;
            }

            return await Task.Run(async () =>
            {
                try
                {
                    _currentProject = project;
                    _isReady = false;

                    // 1. Prepare run_active.bat
                    var sb = new StringBuilder();
                    sb.AppendLine("@echo off");
                    sb.AppendLine("set CGO_ENABLED=1");
                    sb.AppendLine($"set CC={config.GccPath}");
                    sb.AppendLine($"set PATH={config.MsysBinPath};%PATH%");
                    sb.AppendLine($"cd /d \"{project.Path}\"");

                    if (config.ShowEnvDiagnostics)
                    {
                        sb.AppendLine("echo ==============================");
                        sb.AppendLine("echo Go Environment Diagnostics");
                        sb.AppendLine("echo ==============================");
                        sb.AppendLine("go env CGO_ENABLED");
                        sb.AppendLine("go env CC");
                        sb.AppendLine("gcc --version");
                        sb.AppendLine("echo ==============================");
                    }

                    sb.AppendLine(config.MakeCommand);
                    File.WriteAllText(_runnerScriptPath, sb.ToString(), Encoding.ASCII);

                    // 2. Configure ProcessStartInfo
                    var psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c \"\"{_runnerScriptPath}\"\"",
                        WorkingDirectory = project.Path,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    };

                    psi.Environment["CGO_ENABLED"] = "1";
                    psi.Environment["CC"] = config.GccPath;
                    var existingPath = psi.Environment.ContainsKey("PATH")
                        ? psi.Environment["PATH"]
                        : Environment.GetEnvironmentVariable("PATH");
                    psi.Environment["PATH"] = config.MsysBinPath + ";" + existingPath;

                    var proc = new Process
                    {
                        StartInfo = psi,
                        EnableRaisingEvents = true
                    };

                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                        {
                            OutputReceived?.Invoke(e.Data, false);
                            CheckOutputForReadiness(project, e.Data);
                        }
                    };

                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                        {
                            OutputReceived?.Invoke(e.Data, true);
                        }
                    };

                    proc.Exited += (s, e) =>
                    {
                        var exitCode = -1;
                        try { exitCode = proc.ExitCode; } catch { }

                        _monitorCts?.Cancel();

                        lock (_lock)
                        {
                            _runningProcess = null;
                            _isReady = false;
                        }

                        OutputReceived?.Invoke($"[INFO] Process terminated with exit code: {exitCode}", false);
                        ProcessFinished?.Invoke(project, exitCode);
                    };

                    lock (_lock)
                    {
                        _runningProcess = proc;
                    }

                    var started = proc.Start();
                    if (!started)
                    {
                        lock (_lock)
                        {
                            _runningProcess = null;
                            _currentProject = null;
                            _isReady = false;
                        }
                        OutputReceived?.Invoke("[ERROR] Failed to launch cmd.exe process.", true);
                        return false;
                    }

                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();

                    // Initial verification: Ensure process did not crash immediately
                    await Task.Delay(300);

                    if (proc.HasExited)
                    {
                        var code = proc.ExitCode;
                        lock (_lock)
                        {
                            _runningProcess = null;
                            _currentProject = null;
                            _isReady = false;
                        }
                        OutputReceived?.Invoke($"[ERROR] Process exited immediately with code {code}.", true);
                        ProcessFinished?.Invoke(project, code);
                        return false;
                    }

                    // Process launched: Status is initially Waiting (compiling CGO)
                    ProcessStarted?.Invoke(project);
                    OutputReceived?.Invoke($"[INFO] Process started (PID: {proc.Id}). Compiling CGO & waiting for service to be ready...", false);

                    // Start background tree monitor to detect when main.exe starts
                    _monitorCts?.Cancel();
                    _monitorCts = new CancellationTokenSource();
                    _ = MonitorProcessReadinessAsync(proc.Id, project, _monitorCts.Token);

                    return true;
                }
                catch (Exception ex)
                {
                    lock (_lock)
                    {
                        _runningProcess = null;
                        _currentProject = null;
                        _isReady = false;
                    }
                    OutputReceived?.Invoke($"[ERROR] Exception starting process: {ex.Message}", true);
                    return false;
                }
            });
        }

        private async Task MonitorProcessReadinessAsync(int rootPid, ProjectRecord project, CancellationToken ct)
        {
            var start = DateTime.UtcNow;

            while (!ct.IsCancellationRequested && IsRunning)
            {
                try
                {
                    await Task.Delay(1500, ct).ConfigureAwait(false);

                    if (!IsRunning || _isReady) break;

                    if (ProcessTreeHelper.HasCompiledAppStarted(rootPid))
                    {
                        MarkAsReady(project, "Application binary started and active in background");
                        break;
                    }

                    // Fallback timeout: after 35s of running without exit, service is considered active
                    if ((DateTime.UtcNow - start).TotalSeconds > 35)
                    {
                        MarkAsReady(project, "Compilation period elapsed; service active");
                        break;
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    // Ignore transient errors
                }
            }
        }

        private void CheckOutputForReadiness(ProjectRecord project, string line)
        {
            if (_isReady || !IsRunning) return;

            var lower = line.ToLowerInvariant();
            if (lower.Contains("listening") || lower.Contains("started") || lower.Contains("server on") ||
                lower.Contains("connected to") || lower.Contains("kafka consumer") || lower.Contains("gin-gonic") ||
                lower.Contains("fiber") || lower.Contains("echo router") || lower.Contains("ready for"))
            {
                MarkAsReady(project, $"Output indicator detected: '{line.Trim()}'");
            }
        }

        private void MarkAsReady(ProjectRecord project, string reason)
        {
            lock (_lock)
            {
                if (_isReady || !IsRunning) return;
                _isReady = true;
            }

            OutputReceived?.Invoke($"[READY] Service is ready and listening! ({reason})", false);
            ProcessReady?.Invoke(project);
        }

        public void Stop()
        {
            _monitorCts?.Cancel();

            Process? procToKill = null;
            lock (_lock)
            {
                if (_runningProcess == null)
                {
                    OutputReceived?.Invoke("[INFO] No running process to stop.", false);
                    return;
                }
                procToKill = _runningProcess;
                _runningProcess = null;
                _isReady = false;
            }

            Task.Run(() =>
            {
                try
                {
                    if (procToKill != null && !procToKill.HasExited)
                    {
                        var pid = procToKill.Id;
                        OutputReceived?.Invoke($"[INFO] Killing process tree (PID: {pid})...", false);

                        try
                        {
                            var psi = new ProcessStartInfo("taskkill", $"/F /T /PID {pid}")
                            {
                                CreateNoWindow = true,
                                UseShellExecute = false
                            };
                            var killer = Process.Start(psi);
                            killer?.WaitForExit(2000);
                        }
                        catch { }

                        try
                        {
                            if (!procToKill.HasExited)
                            {
                                procToKill.Kill(entireProcessTree: true);
                            }
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    OutputReceived?.Invoke($"[WARN] Notice during stop: {ex.Message}", false);
                }
            });
        }

        public static int ForceKillAllLingeringProcesses()
        {
            var killedCount = 0;
            var targetNames = new[] { "make", "go" };

            foreach (var name in targetNames)
            {
                try
                {
                    var processes = Process.GetProcessesByName(name);
                    foreach (var p in processes)
                    {
                        try
                        {
                            var psi = new ProcessStartInfo("taskkill", $"/F /T /PID {p.Id}")
                            {
                                CreateNoWindow = true,
                                UseShellExecute = false
                            };
                            Process.Start(psi)?.WaitForExit(1000);
                            killedCount++;
                        }
                        catch { }
                    }
                }
                catch { }
            }

            return killedCount;
        }

        public void Dispose()
        {
            _monitorCts?.Cancel();
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
