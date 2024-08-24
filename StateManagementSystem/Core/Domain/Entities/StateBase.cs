using StateManagementSystem.Core.Domain.Interfaces;

namespace StateManagementSystem.Core.Domain.Entities
{
    public abstract class StateBase<TContext, TStateId> : IState<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public virtual void Enter(TContext context) { }
        public virtual void Update(TContext context) { }
        public virtual void Exit(TContext context) { }
    }
}