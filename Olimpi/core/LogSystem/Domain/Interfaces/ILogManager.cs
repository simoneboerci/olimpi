using Olimpi.Core.LogSystem.Domain.Entities;
using Olimpi.Core.LogSystem.Domain.Enums;
using Olimpi.Core.LogSystem.Domain.Interfaces;

namespace Olimpi.core.LogSystem.Domain.Interfaces
{
    public interface ILogManager
    {
        ILogger GetLogger(LogContext context);
        void SetLogLevel(LogLevel level, LogContext context = null);
        void AddSink(ILogSink sink, LogContext contetx = null);
        void RemoveSink(ILogSink sink, LogContext context = null);
        LogContext CreateContext(string name);
    }
}