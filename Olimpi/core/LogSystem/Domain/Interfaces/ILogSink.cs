using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Domain.Interfaces{
    public interface ILogSink{
        public void Write(string formattedLog);
        public Task WriteAsync(string formattedLog);
    }
}