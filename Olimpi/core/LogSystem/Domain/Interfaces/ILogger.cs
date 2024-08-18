using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Domain.Interfaces
{
    public interface ILogger
    {
        public void Log(ILogEntry entry);
        public Task LogAsync(ILogEntry entry);
    }
}