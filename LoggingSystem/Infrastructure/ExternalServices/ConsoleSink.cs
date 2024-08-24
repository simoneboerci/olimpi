using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Infrastructure.ExternalServices
{
    public class ConsoleSink : ILogSink
    {
        public void Write(string formattedLog) => Console.WriteLine(formattedLog);

        public Task WriteAsync(string formattedLog) => Task.Run(() => Console.WriteLine(formattedLog));
    }
}