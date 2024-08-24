using StateManagementSystem.Core.Domain.Interfaces;

namespace StateManagementSystem.Core.Domain.ValueObjects
{
    public readonly struct StateTransition<TContext, TStateId>(TStateId fromState, TStateId toState, Func<TContext, bool> condition) where TContext : IStateMachineContext where TStateId : Enum
    {
        public TStateId FromState { get; } = fromState;
        public TStateId ToState { get; } = toState;
        public Func<TContext, bool> Condition { get; } = condition;
    }
}