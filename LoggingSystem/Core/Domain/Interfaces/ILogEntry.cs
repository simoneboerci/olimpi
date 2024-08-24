using LoggingSystem.Core.Domain.Entities;
using LoggingSystem.Core.Domain.Enums;

namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ILogEntry
    {
        public DateTime Timestamp { get; }
        public LogLevel Level { get; }
        public string Message { get; }
        public LogContext Context { get; }
        public IDictionary<string, object>? AdditionalData { get; }
    }
}