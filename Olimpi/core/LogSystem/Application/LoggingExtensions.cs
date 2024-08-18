using System.Collections.Generic;

namespace Olimpi.Core.LogSystem.Application
{
    public static class LoggingExtensions{
        public static void Debug(this ILogger logger, string message, IDictionary<string, object> additionalData = null){
            logger.Log(new LogEntry { Level = LogLevel.Debug, Message = message, AdditionalData = additionalData });
        }

        public static void Info(this ILogger logger, string message, IDictionary<string, object> additionalData = null){
            logger.Log(new LogEntry { Level = LogLevel.Info, Message = message, AdditionalData = additionalData });
        }

        public static void Warning(this ILogger logger, string message, IDictionary<string, object> additionalData = null){
            logger.Log(new LogEntry { Level = LogLevel.Warning, Message = message, AdditionalData = additionalData });
        }

        public static void Error(this ILogger logger, string message, IDictionary<string, object> additionalData = null){
            logger.Log(new LogEntry { Level = LogLevel.Error, Message = message, AdditionalData = additionalData });
        }
        
        public static void Critical(this ILogger logger, string message, IDictionary<string, object> additionalData = null){
            logger.Log(new LogEntry { Level = LogLevel.Critical, Message = message, AdditionalData = additionalData });
        }
    }
}