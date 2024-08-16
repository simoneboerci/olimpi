using System;
using Olimpi.features.custom_life_cycle.domain.interfaces;

namespace Olimpi.features.custom_life_cycle.domain.entities{
    public abstract class BaseState<TContext, TStateId> : IState<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public virtual void Enter(TContext context) {}
        public virtual void Update(TContext context) {}
        public virtual void Exit(TContext context) {}
    }
}