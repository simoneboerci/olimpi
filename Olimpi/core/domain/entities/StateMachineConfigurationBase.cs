using System;
using System.Collections.Generic;
using Olimpi.Core.Domain.Interfaces;

namespace Olimpi.Core.Domain.Entities{
    public abstract class StateMachineConfigurationBase<TContext, TStateId> : IStateMachineConfiguration<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum{
        
        public IDictionary<TStateId, IState<TContext, TStateId>> States { get; }
        public IList<StateTransition<TContext, TStateId>> Transitions { get; }
        public TStateId InitialState { get; }

        public StateMachineConfigurationBase(
            IDictionary<TStateId, IState<TContext, TStateId>> states,
            TStateId initialState,
            IList<StateTransition<TContext, TStateId>> transitions = null
        ){
            States = states;
            Transitions = transitions ?? new List<StateTransition<TContext, TStateId>>();
            InitialState = initialState;
        }
    }
}