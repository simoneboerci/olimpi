namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ILogSink
    {
        public void Write(string formattedLog);
        public Task WriteAsync(string formattedLog);
    }
}