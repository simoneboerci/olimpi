using System;
using System.Collections.Generic;
using Olimpi.Core.LogSystem.Domain.Entities;
using Olimpi.Core.LogSystem.Domain.Enums;

namespace Olimpi.Core.LogSystem.Domain.Interfaces
{
    public interface ILogEntry
    {
        public DateTime Timestamp { get; }
        public LogLevel Level { get; }
        public string Message { get; }
        public LogContext Context { get; }
        public IDictionary<string, object> AdditionalData { get; }
    }
}