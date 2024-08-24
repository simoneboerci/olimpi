using LoggingSystem.Core.Domain.Entities;
using LoggingSystem.Core.Domain.Enums;
using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Core.Domain.ValueObjects
{
    public struct LogEntry : ILogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public LogContext Context { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}