using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using kafkaRunnerWindows.Models;

namespace kafkaRunnerWindows.Storage
{
    public class JsonProjectStore
    {
        private readonly string _dataDir;
        private readonly string _filePath;

        public JsonProjectStore()
        {
            _dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kafka-runner-windows");
            Directory.CreateDirectory(_dataDir);
            _filePath = Path.Combine(_dataDir, "projects.json");
        }

        public List<ProjectRecord> LoadAll()
        {
            if (!File.Exists(_filePath)) return new List<ProjectRecord>();
            try
            {
                var txt = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<ProjectRecord>>(txt);
                return list ?? new List<ProjectRecord>();
            }
            catch
            {
                return new List<ProjectRecord>();
            }
        }

        public void SaveAll(IEnumerable<ProjectRecord> projects)
        {
            try
            {
                var tmp = _filePath + ".tmp";
                var txt = JsonSerializer.Serialize(projects, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(tmp, txt);

                if (File.Exists(_filePath))
                {
                    try
                    {
                        File.Copy(_filePath, _filePath + ".bak", true);
                    }
                    catch (FileNotFoundException)
                    {
                        // Ignore if original vanished
                    }
                }
                File.Move(tmp, _filePath, true);
            }
            catch
            {
                // Ignore transient write errors
            }
        }

        public void Add(ProjectRecord project)
        {
            var list = LoadAll();
            var existing = list.FirstOrDefault(p => string.Equals(p.Path, project.Path, StringComparison.OrdinalIgnoreCase));
            if (existing == null)
            {
                list.Add(project);
                SaveAll(list);
            }
        }

        public void Update(ProjectRecord project)
        {
            var list = LoadAll();
            var index = list.FindIndex(p => p.Id == project.Id);
            if (index >= 0)
            {
                list[index] = project;
                SaveAll(list);
            }
        }

        public void Remove(Guid id)
        {
            var list = LoadAll();
            list.RemoveAll(p => p.Id == id);
            SaveAll(list);
        }
    }
}
