using System.Collections.Concurrent;
using LoggingSystem.Core.Domain.Entities;
using LoggingSystem.Core.Domain.Enums;
using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Core.Application.Services
{
    public class LogManager(
        ILogFormatter formatter,
        ISensitiveDataFiler sensitiveDataFilter,
        ConcurrentDictionary<LogContext, ILogger>? loggers = null,
        ConcurrentDictionary<LogContext, List<ILogSink>>? sinks = null,
        ConcurrentDictionary<LogContext, LogLevel>? logLevels = null,
        LogContext? globalContext = null
        ) : ILogManager
    {
        private readonly ConcurrentDictionary<LogContext, ILogger> _loggers = loggers ?? new();
        private readonly ConcurrentDictionary<LogContext, List<ILogSink>> _sinks = sinks ?? new();
        private readonly ConcurrentDictionary<LogContext, LogLevel> _logLevels = logLevels ?? new();

        private readonly ILogFormatter _formatter = formatter;
        private readonly ISensitiveDataFiler _sensitiveDataFilter = sensitiveDataFilter;

        private LogContext _globalContext = globalContext ?? LogContext.Create(_defaultGlobalContext);

        private const string _defaultGlobalContext = "Global";

        public ILogger GetLogger(LogContext context) => _loggers.GetOrAdd(context, c => new Logger(c, this));

        public void SetLogLevel(LogLevel level, LogContext? context = null)
        {
            if (context != null) _logLevels[context] = level;
            else _logLevels[_globalContext] = level;
        }

        public void AddSink(ILogSink sink, LogContext? context = null)
        {
            if (context != null) _sinks.GetOrAdd(context, _ => []).Add(sink);
            else _sinks.GetOrAdd(_globalContext, _ => []).Add(sink);
        }

        public void RemoveSink(ILogSink sink, LogContext? context = null)
        {
            if (context != null)
            {
                if (_sinks.TryGetValue(context, out var sinks)) sinks.Remove(sink);
            }
            else
            {
                if (_sinks.TryGetValue(_globalContext, out var sinks)) sinks.Remove(sink);
            }
        }

        public LogContext CreateContext(string name) => LogContext.Create(name);

        public void Log(ILogEntry entry)
        {
            if (ShouldLog(entry))
            {
                var formattedLog = _formatter.Format(entry);
                var censoredLog = _sensitiveDataFilter.ApplyCensorship(formattedLog, true);

                foreach (var sink in GetSinks(entry.Context))
                {
                    sink.Write(censoredLog);
                }
            }
        }

        public async Task LogAsync(ILogEntry entry)
        {
            if (ShouldLog(entry))
            {
                var formattedLog = _formatter.Format(entry);
                var censoredLog = _sensitiveDataFilter.ApplyCensorship(formattedLog, true);
                var tasks = GetSinks(entry.Context).Select(sink => sink.WriteAsync(censoredLog));

                await Task.WhenAll(tasks);
            }
        }

        private bool ShouldLog(ILogEntry entry)
        {
            return _logLevels.TryGetValue(entry.Context, out var contextLevel)
                ? entry.Level >= contextLevel
                : _logLevels.TryGetValue(_globalContext, out var globalLevel) && entry.Level >= globalLevel;
        }

        private IEnumerable<ILogSink> GetSinks(LogContext context)
        {
            if (_sinks.TryGetValue(context, out var contextSinks))
            {
                foreach (var sink in contextSinks) yield return sink;
            }

            if (_sinks.TryGetValue(_globalContext, out var globalSinks))
            {
                foreach (var sink in globalSinks) yield return sink;
            }
        }
    }
}