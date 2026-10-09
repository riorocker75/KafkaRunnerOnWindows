using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using kafkaRunnerWindows.Forms;
using kafkaRunnerWindows.Importer;
using kafkaRunnerWindows.Models;
using kafkaRunnerWindows.Services;
using kafkaRunnerWindows.Storage;

namespace kafkaRunnerWindows
{
    public partial class Form1 : Form
    {
        private readonly JsonProjectStore _projectStore = new();
        private readonly ConfigStore _configStore = new();
        private readonly ProcessRunnerService _runner = new();

        private List<ProjectRecord> _projects = new();
        private RunnerConfig _config;

        public Form1()
        {
            InitializeComponent();

            try
            {
                var exePath = Environment.ProcessPath ?? Application.ExecutablePath;
                if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
                {
                    var ico = Icon.ExtractAssociatedIcon(exePath);
                    if (ico != null) this.Icon = ico;
                }
                else
                {
                    var localIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico");
                    if (File.Exists(localIco)) this.Icon = new Icon(localIco);
                }
            }
            catch { }

            _config = _configStore.Load();

            // Wire up runner service events
            _runner.OutputReceived += OnRunnerOutputReceived;
            _runner.ProcessStarted += OnRunnerProcessStarted;
            _runner.ProcessReady += OnRunnerProcessReady;
            _runner.ProcessFinished += OnRunnerProcessFinished;

            // Load saved projects
            LoadProjects();
        }

        private void LoadProjects()
        {
            _projects = _projectStore.LoadAll();

            foreach (var p in _projects)
            {
                if (_runner.CurrentProject?.Id == p.Id && _runner.IsRunning)
                {
                    p.Status = _runner.IsReady ? "Running" : "Waiting...";
                }
                else
                {
                    p.Status = Directory.Exists(p.Path) ? "Ready" : "Folder Missing";
                }
            }

            RebuildGrid();
        }

        private void RebuildGrid()
        {
            var selectedId = GetSelectedProject()?.Id;

            dgvProjects.Rows.Clear();

            foreach (var p in _projects)
            {
                var rowIndex = dgvProjects.Rows.Add(
                    p.Name,
                    string.IsNullOrWhiteSpace(p.FeatureTask) ? "-" : p.FeatureTask,
                    string.IsNullOrWhiteSpace(p.WorkStatus) ? "In Progress" : p.WorkStatus,
                    p.Status,
                    p.Path,
                    p.LastRunAt?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") ?? "-"
                );

                var row = dgvProjects.Rows[rowIndex];
                row.Tag = p;
                ApplyRowStyles(row, p);
            }

            // Restore selection if possible
            if (selectedId != null)
            {
                foreach (DataGridViewRow row in dgvProjects.Rows)
                {
                    if (row.Tag is ProjectRecord p && p.Id == selectedId)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }

            UpdateControlStates();
        }

        private void UpdateRowStatus(ProjectRecord project, string status)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProjectRecord, string>(UpdateRowStatus), project, status);
                return;
            }

            project.Status = status;
            _projectStore.Update(project);

            foreach (DataGridViewRow row in dgvProjects.Rows)
            {
                if (row.Tag is ProjectRecord p && p.Id == project.Id)
                {
                    row.Cells[3].Value = status;
                    if (project.LastRunAt.HasValue)
                    {
                        row.Cells[5].Value = project.LastRunAt.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    ApplyRowStyles(row, project);
                    break;
                }
            }

            UpdateControlStates();
        }

        private void ApplyRowStyles(DataGridViewRow row, ProjectRecord project)
        {
            // Work Status column (Index 2)
            var workStatusCell = row.Cells[2];
            var ws = project.WorkStatus?.ToLowerInvariant() ?? "";
            if (ws.Contains("done") || ws.Contains("complete"))
            {
                workStatusCell.Style.ForeColor = Color.FromArgb(25, 135, 84); // Green
                workStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Bold);
            }
            else if (ws.Contains("progress"))
            {
                workStatusCell.Style.ForeColor = Color.FromArgb(13, 110, 253); // Blue
                workStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Bold);
            }
            else if (ws.Contains("test"))
            {
                workStatusCell.Style.ForeColor = Color.FromArgb(217, 119, 6); // Amber
                workStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Regular);
            }
            else
            {
                workStatusCell.Style.ForeColor = Color.FromArgb(108, 117, 125);
                workStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Regular);
            }

            // Runner Status column (Index 3)
            var runnerStatusCell = row.Cells[3];
            switch (project.Status)
            {
                case "Running":
                    runnerStatusCell.Style.ForeColor = Color.FromArgb(25, 135, 84);
                    runnerStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Bold);
                    break;
                case "Waiting...":
                case "Starting...":
                    runnerStatusCell.Style.ForeColor = Color.FromArgb(245, 158, 11);
                    runnerStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Bold);
                    break;
                case "Folder Missing":
                case "Failed":
                    runnerStatusCell.Style.ForeColor = Color.FromArgb(220, 53, 69);
                    runnerStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Regular);
                    break;
                case "Stopped":
                    runnerStatusCell.Style.ForeColor = Color.FromArgb(108, 117, 125);
                    runnerStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Regular);
                    break;
                default:
                    runnerStatusCell.Style.ForeColor = Color.FromArgb(70, 80, 95);
                    runnerStatusCell.Style.Font = new Font(dgvProjects.Font, FontStyle.Regular);
                    break;
            }
        }

        private ProjectRecord? GetSelectedProject()
        {
            if (dgvProjects.SelectedRows.Count > 0)
            {
                return dgvProjects.SelectedRows[0].Tag as ProjectRecord;
            }
            return null;
        }

        private void UpdateControlStates()
        {
            if (IsDisposed) return;

            var selected = GetSelectedProject();
            var isRunning = _runner.IsRunning;

            // Run button state & colors
            var canRun = !isRunning && selected != null && selected.Status != "Folder Missing";
            btnRun.Enabled = canRun;
            btnRun.BackColor = canRun ? Color.FromArgb(25, 135, 84) : Color.FromArgb(108, 117, 125); // Green when ready, Gray when running/disabled
            btnRun.ForeColor = Color.White;

            // Stop button state & colors
            btnStop.Enabled = isRunning;
            btnStop.BackColor = isRunning ? Color.FromArgb(220, 53, 69) : Color.FromArgb(108, 117, 125); // Red when active, Gray when stopped
            btnStop.ForeColor = Color.White;

            btnRemove.Enabled = !isRunning && selected != null;
            btnBrowseFolder.Enabled = !isRunning;
            btnAddProject.Enabled = !isRunning;

            miRun.Enabled = btnRun.Enabled;
            miStop.Enabled = btnStop.Enabled;
            miEdit.Enabled = selected != null;
            miRemove.Enabled = btnRemove.Enabled;

            // Top status banner
            if (isRunning && _runner.CurrentProject != null)
            {
                if (_runner.IsReady)
                {
                    lblCurrentStatus.Text = $"● Running: {_runner.CurrentProject.Name}";
                    lblCurrentStatus.BackColor = Color.FromArgb(25, 135, 84); // Green
                    lblCurrentStatus.ForeColor = Color.White;
                }
                else
                {
                    lblCurrentStatus.Text = $"⏳ Waiting: {_runner.CurrentProject.Name} (Compiling CGO...)";
                    lblCurrentStatus.BackColor = Color.FromArgb(245, 158, 11); // Amber / Yellow
                    lblCurrentStatus.ForeColor = Color.White;
                }
            }
            else
            {
                lblCurrentStatus.Text = "Status: Idle";
                lblCurrentStatus.BackColor = Color.FromArgb(40, 48, 60);
                lblCurrentStatus.ForeColor = Color.FromArgb(180, 195, 215);
            }
        }

        #region Add & Browse Project

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.Description = "Select Go / Kafka Project Root Directory";
            dlg.UseDescriptionForTitle = true;

            if (!string.IsNullOrWhiteSpace(txtProjectPath.Text) && Directory.Exists(txtProjectPath.Text.Trim()))
            {
                dlg.SelectedPath = txtProjectPath.Text.Trim();
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtProjectPath.Text = dlg.SelectedPath;
                OpenAddProjectDialog(dlg.SelectedPath);
            }
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            OpenAddProjectDialog(txtProjectPath.Text?.Trim() ?? "");
        }

        private void txtProjectPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAddProject_Click(sender, e);
            }
        }

        private void OpenAddProjectDialog(string initialPath)
        {
            using var dlg = new AddProjectDialog(initialPath);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var rec = dlg.Project;

                if (_projects.Any(x => string.Equals(x.Path, rec.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Project path is already in the list.", "Add Project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _projectStore.Add(rec);
                _projects.Add(rec);
                RebuildGrid();
                txtProjectPath.Clear();

                AppendConsole($"[PROJECT ADDED] Added '{rec.Name}' ({rec.FeatureTask} - {rec.WorkStatus}) -> {rec.Path}", Color.FromArgb(78, 201, 176));
            }
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProject();
            if (selected == null) return;

            using var dlg = new AddProjectDialog(existing: selected);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _projectStore.Update(selected);
                RebuildGrid();
                AppendConsole($"[PROJECT UPDATED] Updated '{selected.Name}': Task='{selected.FeatureTask}', Status='{selected.WorkStatus}'", Color.FromArgb(78, 201, 176));
            }
        }

        #endregion

        #region Run & Stop

        private async void btnRun_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProject();
            if (selected == null)
            {
                MessageBox.Show("Please select a project from the table to run.", "Run", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_runner.IsRunning)
            {
                MessageBox.Show("A process is already running. Please stop it first before starting another.", "Run", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(selected.Path))
            {
                MessageBox.Show($"The folder does not exist:\n{selected.Path}", "Folder Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateRowStatus(selected, "Folder Missing");
                return;
            }

            // Immediately mark as Waiting... so user knows CGO is compiling
            selected.LastRunAt = DateTime.UtcNow;
            UpdateRowStatus(selected, "Waiting...");

            AppendConsole("\n=======================================================", Color.FromArgb(120, 120, 120));
            AppendConsole($"  STARTING: {selected.Name}", Color.FromArgb(78, 201, 176));
            if (!string.IsNullOrWhiteSpace(selected.FeatureTask))
            {
                AppendConsole($"  FEATURE : {selected.FeatureTask} ({selected.WorkStatus})", Color.FromArgb(255, 193, 7));
            }
            AppendConsole($"  PATH    : {selected.Path}", Color.FromArgb(156, 220, 254));
            AppendConsole("=======================================================\n", Color.FromArgb(120, 120, 120));

            var started = await _runner.StartAsync(selected, _config);

            if (!started)
            {
                UpdateRowStatus(selected, "Failed");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!_runner.IsRunning)
            {
                return;
            }

            btnStop.Enabled = false;
            btnStop.BackColor = Color.FromArgb(108, 117, 125);
            AppendConsole("\n[STOP REQUESTED] Terminating process...", Color.FromArgb(244, 135, 113));
            _runner.Stop();
        }

        #endregion

        #region Runner Callbacks

        private void OnRunnerOutputReceived(string text, bool isError)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<string, bool>(OnRunnerOutputReceived), text, isError);
                }
                catch { }
                return;
            }

            var color = isError ? Color.FromArgb(244, 135, 113) : Color.FromArgb(220, 220, 220);
            AppendConsole(text, color);
        }

        private void OnRunnerProcessStarted(ProjectRecord project)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<ProjectRecord>(OnRunnerProcessStarted), project);
                }
                catch { }
                return;
            }

            UpdateRowStatus(project, "Waiting...");
        }

        private void OnRunnerProcessReady(ProjectRecord project)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<ProjectRecord>(OnRunnerProcessReady), project);
                }
                catch { }
                return;
            }

            UpdateRowStatus(project, "Running");
        }

        private void OnRunnerProcessFinished(ProjectRecord project, int exitCode)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<ProjectRecord, int>(OnRunnerProcessFinished), project, exitCode);
                }
                catch { }
                return;
            }

            var status = exitCode == 0 ? "Ready" : "Stopped";
            UpdateRowStatus(project, status);
        }

        #endregion

        #region Toolbar Actions

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProject();
            if (selected == null) return;

            var confirm = MessageBox.Show($"Remove '{selected.Name}' from project list?\n(Files on disk will NOT be deleted)", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _projectStore.Remove(selected.Id);
            _projects.RemoveAll(x => x.Id == selected.Id);
            RebuildGrid();

            AppendConsole($"[PROJECT REMOVED] '{selected.Name}' removed from list.", Color.FromArgb(180, 180, 180));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProjects();
            AppendConsole("[INFO] Refreshed all project folder statuses.", Color.FromArgb(156, 220, 254));
        }

        private void btnForceKill_Click(object sender, EventArgs e)
        {
            var killed = ProcessRunnerService.ForceKillAllLingeringProcesses();
            AppendConsole($"[FORCE KILL] Terminated {killed} lingering process(es) (make/go).", Color.FromArgb(255, 193, 7));
            LoadProjects();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.Title = "Import Projects from Text File";

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var added = 0;
            foreach (var path in TextFileImporter.Parse(dlg.FileName))
            {
                if (_projects.Any(x => string.Equals(x.Path, path, StringComparison.OrdinalIgnoreCase))) continue;

                var rec = new ProjectRecord
                {
                    Path = path,
                    Name = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)),
                    AddedAt = DateTime.UtcNow,
                    Status = Directory.Exists(path) ? "Ready" : "Folder Missing",
                    FeatureTask = "",
                    WorkStatus = "In Progress"
                };

                _projectStore.Add(rec);
                _projects.Add(rec);
                added++;
            }

            if (added > 0)
            {
                RebuildGrid();
                AppendConsole($"[IMPORT] Successfully imported {added} project(s) from {Path.GetFileName(dlg.FileName)}.", Color.FromArgb(78, 201, 176));
                MessageBox.Show($"Imported {added} project(s).", "Import Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No new paths found in the imported file.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var form = new SettingsForm(_config);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _config = form.Config;
                _configStore.Save(_config);
                AppendConsole("[SETTINGS] Runner configuration saved successfully.", Color.FromArgb(78, 201, 176));
            }
        }

        #endregion

        #region Grid & Context Menu Events

        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
        }

        private void dgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnRun.Enabled)
            {
                btnRun_Click(sender, EventArgs.Empty);
            }
        }

        private void miOpenExplorer_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProject();
            if (selected == null) return;

            if (Directory.Exists(selected.Path))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"\"{selected.Path}\"",
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("The project directory does not exist on disk.", "Cannot Open", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void miOpenCmd_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProject();
            if (selected == null) return;

            if (Directory.Exists(selected.Path))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = selected.Path,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("The project directory does not exist on disk.", "Cannot Open", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Console Logging

        private void AppendConsole(string message, Color color)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<string, Color>(AppendConsole), message, color);
                }
                catch { }
                return;
            }

            rtbOutput.SelectionStart = rtbOutput.TextLength;
            rtbOutput.SelectionLength = 0;
            rtbOutput.SelectionColor = color;
            rtbOutput.AppendText(message + Environment.NewLine);
            rtbOutput.SelectionColor = rtbOutput.ForeColor;

            if (chkAutoScroll.Checked)
            {
                rtbOutput.ScrollToCaret();
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
        }

        private void btnCopyLog_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbOutput.Text)) return;
            Clipboard.SetText(rtbOutput.Text);
            MessageBox.Show("Console output copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Form Lifecycle

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_runner.IsRunning)
            {
                var confirm = MessageBox.Show(
                    "A project runner is currently active. Closing the application will terminate the running process.\n\nDo you want to stop the runner and exit?",
                    "Runner Active",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    _runner.Stop();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            _runner.Dispose();
        }

        #endregion
    }
}
