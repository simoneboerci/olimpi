namespace Olimpi.Core.LogSystem.Domain.Entities
{
    public class LogEntry : ILogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public LogContext Context { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}