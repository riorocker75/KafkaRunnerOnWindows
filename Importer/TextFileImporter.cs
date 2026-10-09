using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kafkaRunnerWindows.Importer
{
    public static class TextFileImporter
    {
        public static IEnumerable<string> Parse(string filePath)
        {
            var results = new List<string>();
            if (!File.Exists(filePath)) return results;
            foreach (var raw in File.ReadAllLines(filePath))
            {
                var line = raw?.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("#")) continue;
                // Expand environment variables and get full path
                try
                {
                    var expanded = Environment.ExpandEnvironmentVariables(line);
                    var full = Path.GetFullPath(expanded);
                    results.Add(full);
                }
                catch
                {
                    // skip invalid paths
                    continue;
                }
            }
            return results;
        }
    }
}
