using System;

namespace kafkaRunnerWindows.Models
{
    public class RunnerConfig
    {
        public string MsysBinPath { get; set; } = @"C:\msys64\mingw64\bin";
        public string GccPath { get; set; } = @"C:\msys64\mingw64\bin\gcc.exe";
        public string MakeCommand { get; set; } = "make run";
        public bool ShowEnvDiagnostics { get; set; } = true;
    }
}
