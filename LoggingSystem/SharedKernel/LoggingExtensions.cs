using LoggingSystem.Core.Domain.Enums;
using LoggingSystem.Core.Domain.Interfaces;
using LoggingSystem.Core.Domain.ValueObjects;

namespace LoggingSystem.SharedKernel
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, IDictionary<string, object>? additionalData = null)
        {
            logger.Log(new LogEntry { Level = LogLevel.Debug, Message = message, AdditionalData = additionalData ?? new Dictionary<string, object>() });
        }

        public static void Info(this ILogger logger, string message, IDictionary<string, object>? additionalData = null)
        {
            logger.Log(new LogEntry { Level = LogLevel.Info, Message = message, AdditionalData = additionalData ?? new Dictionary<string, object>() });
        }

        public static void Warning(this ILogger logger, string message, IDictionary<string, object>? additionalData = null)
        {
            logger.Log(new LogEntry { Level = LogLevel.Warning, Message = message, AdditionalData = additionalData ?? new Dictionary<string, object>() });
        }

        public static void Error(this ILogger logger, string message, IDictionary<string, object>? additionalData = null)
        {
            logger.Log(new LogEntry { Level = LogLevel.Error, Message = message, AdditionalData = additionalData ?? new Dictionary<string, object>() });
        }

        public static void Critical(this ILogger logger, string message, IDictionary<string, object>? additionalData = null)
        {
            logger.Log(new LogEntry { Level = LogLevel.Critical, Message = message, AdditionalData = additionalData ?? new Dictionary<string, object>() });
        }
    }
}