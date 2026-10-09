using System;

namespace kafkaRunnerWindows.Models
{
    public class ProjectRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string FeatureTask { get; set; } = string.Empty;
        public string WorkStatus { get; set; } = "In Progress";
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Ready";
        public DateTime? LastRunAt { get; set; }
    }
}
