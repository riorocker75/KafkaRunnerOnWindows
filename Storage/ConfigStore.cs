using System;
using System.IO;
using System.Text.Json;
using kafkaRunnerWindows.Models;

namespace kafkaRunnerWindows.Storage
{
    public class ConfigStore
    {
        private readonly string _filePath;

        public ConfigStore()
        {
            var dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kafka-runner-windows");
            Directory.CreateDirectory(dataDir);
            _filePath = Path.Combine(dataDir, "config.json");
        }

        public RunnerConfig Load()
        {
            if (!File.Exists(_filePath))
            {
                var defaultConfig = new RunnerConfig();
                Save(defaultConfig);
                return defaultConfig;
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                var config = JsonSerializer.Deserialize<RunnerConfig>(json);
                return config ?? new RunnerConfig();
            }
            catch
            {
                return new RunnerConfig();
            }
        }

        public void Save(RunnerConfig config)
        {
            try
            {
                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                // Ignore save failures
            }
        }
    }
}
