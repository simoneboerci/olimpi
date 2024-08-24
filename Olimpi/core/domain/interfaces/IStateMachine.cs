using System;
using Olimpi.Core.Domain.Entities;

namespace Olimpi.Core.Domain.Interfaces
{
    public interface IStateMachine<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public event Action<TStateId, TStateId> StateChanged;

        public void ChangeState(TStateId stateId);
        public void Update();
        public void AddTransition(StateTransition<TContext, TStateId> transition);
        public void AddState(TStateId stateId, IState<TContext, TStateId> state);
    }
}