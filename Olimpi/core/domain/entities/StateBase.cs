using System;
using Olimpi.core.domain.interfaces;

namespace Olimpi.core.domain.entities
{
    public abstract class StateBase<TContext, TStateId> : IState<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public virtual void Enter(TContext context) { }
        public virtual void Update(TContext context) { }
        public virtual void Exit(TContext context) { }
    }
}