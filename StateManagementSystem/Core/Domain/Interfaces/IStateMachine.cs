using StateManagementSystem.Core.Domain.ValueObjects;

namespace StateManagementSystem.Core.Domain.Interfaces
{
    public interface IStateMachine<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public bool HasContext { get; }
        public bool HasStates { get; }
        public bool HasTransitions { get; }

        public TContext Context { get; }
        public TStateId? CurrentStateId { get; }
        public event Action<TStateId?, TStateId> StateChanged;

        public void ChangeState(TStateId stateId);
        public void Update();
        public void AddTransition(StateTransition<TContext, TStateId> transition);
        public void AddState(TStateId stateId, IState<TContext, TStateId> state);
    }
}