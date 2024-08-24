namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ILogFormatter
    {
        public string Format(ILogEntry entry);
    }
}