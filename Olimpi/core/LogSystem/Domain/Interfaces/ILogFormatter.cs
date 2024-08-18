namespace Olimpi.Core.LogSystem.Domain.Interfaces
{
    public interface ILogFormatter
    {
        public string Format(ILogEntry entry);
    }
}