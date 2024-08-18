using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Application
{
    public class LogManager : ILogManager{
        private readonly ConcurrentDictionary<LogContext, ILogger> _loggers;
        private readonly ConcurrentDictionary<LogContext, List<ILogSink>> _sinks;
        private readonly ConcurrentDictionary<LogContext, LogLevel> _logLevels;

        private readonly ILogFormatter _formatter;
        private readonly ISensitiveDataFiler _sensitiveDataFilter;

        private LogContext _globalContext;
    
        private const string _defaultGlobalContext = "Global";

        public LogManager(
            ILogFormatter formatter, 
            ISensitiveDataFiler sensitiveDataFilter,
            ConcurrentDictionary<LogContext, ILogger> loggers = null,
            ConcurrentDictionary<LogContext, List<ILogSink>> sinks = null,
            ConcurrentDictionary<LogContext, LogLevel> logLevels = null,
            LogContext globalContext = null
        ){
            _formatter = formatter;
            _sensitiveDataFilter = sensitiveDataFilter;

            _loggers = loggers ?? new();
            _sinks = sinks ?? new();
            _logLevels = logLevels ?? new();

            _globalContext = globalContext ?? LogContext.Create(_defaultGlobalContext);
        }

        public ILogger GetLogger(LogContext context) => _loggers.GetOrAdd(context, c => new Logger(c, this));

        public void SetLogLevel(LogLevel level, LogContext context = null){
            if(context != null) _logLevels[context] = level;
            else _logLevels[_globalContext] = level;
        }

        public void AddSink(ILogSink sink, LogContext context = null){
            if(context != null) _sinks.GetOrAdd(context, _ => new List<ILogSink>().Add(sink));
            else _sinks.GetOrAdd(_globalContext, _ => new List<ILogSink>().Add(sink));
        }

        public void RemoveSink(ILogSink sink, LogContext context = null){
            if(context != null){
                if(_sinks.TryGetValue(context, out var sinks)) sinks.Remove(sink);
            }else{
                if(_sinks.TryGetValue(_globalContext, out var sinks)) sinks.Remove(sink);
            }
        }

        public LogContext CreateContext(string name) => LogContext.Create(name);

        internal void Log(ILogEntry entry){
            if(ShouldLog(entry)){
                var formattedLog = _formatter.Format(entry);
                var censoredLog = _sensitiveDataFilter.ApplyCensorship(formattedLog, true);

                foreach (var sink in GetSinks(entry.Context))
                {
                    sink.Write(censoredLog);
                }
            }
        }

        internal async Task LogAsync(ILogEntry entry){
            if(ShouldLog(entry)){
                var formattedLog = _formatter.Format(entry);
                var censoredLog = _sensitiveDataFilter.ApplyCensorship(formattedLog, true);
                var tasks = GetSinks(entry.Context).Select(sink => sink.WriteAsync(censoredLog));

                await Task.WhenAll(tasks);
            }
        }

        private bool ShouldLog(ILogEntry entry){
            return _logLevels.TryGetValue(entry.Context, out var contextLevel) 
                ? entry.Level >= contextLevel 
                : _logLevels.TryGetValue(_globalContext, out var globalLevel) && entry.Level >= globalLevel;
        }

        private IEnumerable<ILogSink> GetSinks(LogContext context){
            if(_sinks.TryGetValue(context, out var contextSinks)){
                foreach (var sink in contextSinks) yield return sink;
            }

            if(_sinks.TryGetValue(_globalContext, out var globalSinks)){
                foreach (var sink in globalSinks) yield return sink;
            }
        }
    }
}