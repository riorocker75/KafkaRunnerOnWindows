using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace kafkaRunnerWindows.Services
{
    public static class ProcessTreeHelper
    {
        private const uint TH32CS_SNAPPROCESS = 0x00000002;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        private static readonly HashSet<string> BuildToolNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "cmd.exe",
            "conhost.exe",
            "make.exe",
            "go.exe",
            "gcc.exe",
            "compile.exe",
            "link.exe",
            "asm.exe",
            "cgo.exe",
            "sh.exe",
            "bash.exe"
        };

        public static bool HasCompiledAppStarted(int rootPid)
        {
            var snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
            if (snapshot == IntPtr.Zero || snapshot == new IntPtr(-1))
            {
                return false;
            }

            try
            {
                var parentToChildren = new Dictionary<uint, List<(uint Pid, string Name)>>();
                var pe = new PROCESSENTRY32 { dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32)) };

                if (Process32First(snapshot, ref pe))
                {
                    do
                    {
                        var parentId = pe.th32ParentProcessID;
                        if (!parentToChildren.ContainsKey(parentId))
                        {
                            parentToChildren[parentId] = new List<(uint, string)>();
                        }
                        parentToChildren[parentId].Add((pe.th32ProcessID, pe.szExeFile));
                    }
                    while (Process32Next(snapshot, ref pe));
                }

                // Traverse descendants of rootPid
                var queue = new Queue<uint>();
                queue.Enqueue((uint)rootPid);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (parentToChildren.TryGetValue(current, out var children))
                    {
                        foreach (var child in children)
                        {
                            var name = child.Name;
                            // If we find main.exe or any non-build-tool process, the app is running!
                            if (name.Equals("main.exe", StringComparison.OrdinalIgnoreCase) ||
                                !BuildToolNames.Contains(name))
                            {
                                return true;
                            }
                            queue.Enqueue(child.Pid);
                        }
                    }
                }

                return false;
            }
            finally
            {
                CloseHandle(snapshot);
            }
        }
    }
}
