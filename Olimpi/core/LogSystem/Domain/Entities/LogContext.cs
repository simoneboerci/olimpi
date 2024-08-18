using System;

namespace Olimpi.Core.LogSystem.Domain.Entities
{
    public class LogContext
    {
        public string Name { get; }

        private LogContext(string name) => Name = name;

        public static LogContext Create(string name) => new LogContext(name);

        public override bool Equals(object obj) => obj is LogContext context && Name == context.Name;

        public override int GetHashCode() => HashCode.Combine(Name);
    }
}