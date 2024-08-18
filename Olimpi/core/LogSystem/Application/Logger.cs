using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Application
{
    public class Logger : ILogger{
        private readonly LogContext _context;
        private readonly ILogManager _logManager;

        public Logger(LogContext context, ILogManager logManager){
            _context = context;
            _logManager = logManager;
        }

        public void Log(ILogEntry entry) => _logManager.Log(entry);

        public async Task LogAsync(ILogEntry entry) => await _logManager.LogAsync(entry);
    }
}