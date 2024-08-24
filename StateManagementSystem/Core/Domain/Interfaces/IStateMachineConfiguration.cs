using StateManagementSystem.Core.Domain.ValueObjects;

namespace StateManagementSystem.Core.Domain.Interfaces
{
    public interface IStateMachineConfiguration<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public IDictionary<TStateId, IState<TContext, TStateId>> States { get; }
        public IList<StateTransition<TContext, TStateId>> Transitions { get; }
        public TStateId InitialState { get; }
    }
}