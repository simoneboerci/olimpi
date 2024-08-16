using System;

namespace Olimpi.features.custom_life_cycle.domain.interfaces
{
    public interface IState<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public void Enter(TContext context);
        public void Update(TContext context);
        public void Exit(TContext context);
    }
}
