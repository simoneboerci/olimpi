using Olimpi.Core.LogSystem.Domain.Interfaces;

namespace Olimpi.Core.LogSystem.Infrastructure.Formatters;

public class DefaultFormatter : ILogFormatter
{
    public string Format(ILogEntry entry) => $"[{entry.Timestamp:dd-MM-yyyy HH:mm:ss}] [{entry.Context}] [{entry.Level}] [{entry.Message}]";
}