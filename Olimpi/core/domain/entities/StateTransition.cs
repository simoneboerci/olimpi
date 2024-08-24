using System;
using Olimpi.Core.Domain.Interfaces;

namespace Olimpi.Core.Domain.Entities
{
    public readonly struct StateTransition<TContext, TStateId> : IStateTransition<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public TStateId FromState { get; }
        public TStateId ToState { get; }
        public Func<TContext, bool> Condition { get; }

        public StateTransition(TStateId fromState, TStateId toState, Func<TContext, bool> condition)
        {
            FromState = fromState;
            ToState = toState;
            Condition = condition;
        }
    }
}