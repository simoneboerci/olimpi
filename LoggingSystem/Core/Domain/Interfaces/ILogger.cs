namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ILogger
    {
        public void Log(ILogEntry entry);
        public Task LogAsync(ILogEntry entry);
    }
}