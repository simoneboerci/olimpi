using System;

namespace Olimpi.Core.Domain.Interfaces
{
    public interface IStateTransition<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public TStateId FromState { get; }
        public TStateId ToState { get; }
        public Func<TContext, bool> Condition { get; }
    }
}