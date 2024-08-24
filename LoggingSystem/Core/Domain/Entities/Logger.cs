using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Core.Domain.Entities
{
    public class Logger(LogContext context, ILogManager logManager) : ILogger
    {
        private readonly LogContext _context = context;
        private readonly ILogManager _logManager = logManager;

        public void Log(ILogEntry entry) => _logManager.Log(entry);

        public async Task LogAsync(ILogEntry entry) => await _logManager.LogAsync(entry);
    }
}