using System;
using Olimpi.features.custom_life_cycle.domain.entities;

namespace Olimpi.features.custom_life_cycle.domain.interfaces{
    public interface IStateMachine<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public event Action<TStateId, TStateId> StateChanged;

        public void ChangeState(TStateId stateId);
        public void Update();
        public void AddTransition(StateTransition<TContext, TStateId> transition);
        public void AddState(TStateId stateId, IState<TContext, TStateId> state);     
    }
}