using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Infrastructure.ExternalServices{
    public class DefaultFormatter : ILogFormatter
    {
        public string Format(ILogEntry entry) => $"[{entry.Timestamp:dd-MM-yyyy HH:mm:ss}] [{entry.Context}] [{entry.Level}] [{entry.Message}]";
    }
}
