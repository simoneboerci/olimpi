using LoggingSystem.Core.Domain.Entities;
using LoggingSystem.Core.Domain.Enums;

namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ILogManager
    {
        public ILogger GetLogger(LogContext context);
        public void SetLogLevel(LogLevel level, LogContext? context = null);
        public void AddSink(ILogSink sink, LogContext? contetx = null);
        public void RemoveSink(ILogSink sink, LogContext? context = null);
        public LogContext CreateContext(string name);
        public void Log(ILogEntry entry);
        public Task LogAsync(ILogEntry entry);
    }
}