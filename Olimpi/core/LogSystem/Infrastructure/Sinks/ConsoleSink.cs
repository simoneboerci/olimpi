using System;
using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Infrastructure{
    public class ConsoleSink : ILogSink
    {
        public void Write(string formattedLog) => Console.WriteLine(formattedLog);

        public Task WriteAsync(string formattedLog) => Task.Run(() => Console.WriteLine(formattedLog));
    }
}