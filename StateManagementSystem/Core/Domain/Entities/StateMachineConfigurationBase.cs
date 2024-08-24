using StateManagementSystem.Core.Domain.Interfaces;
using StateManagementSystem.Core.Domain.ValueObjects;

namespace StateManagementSystem.Core.Domain.Entities
{
    public abstract class StateMachineConfigurationBase<TContext, TStateId>(
        IDictionary<TStateId, IState<TContext, TStateId>> states,
        TStateId initialState,
        IList<StateTransition<TContext, TStateId>>? transitions = null
        ) : IStateMachineConfiguration<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {

        public IDictionary<TStateId, IState<TContext, TStateId>> States { get; } = states;
        public IList<StateTransition<TContext, TStateId>> Transitions { get; } = transitions ?? [];
        public TStateId InitialState { get; } = initialState;
    }
}