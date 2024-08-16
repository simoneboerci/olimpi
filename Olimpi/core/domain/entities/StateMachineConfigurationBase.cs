using System;
using System.Collections.Generic;
using Olimpi.core.domain.interfaces;

namespace Olimpi.core.domain.entities{
    public abstract class StateMachineConfigurationBase<TContext, TStateId> : IStateMachineConfiguration<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum{
        
        public Dictionary<TStateId, IState<TContext, TStateId>> States { get; }
        public List<StateTransition<TContext, TStateId>> Transitions { get; }
        public TStateId InitialState { get; }

        public StateMachineConfigurationBase(
            Dictionary<TStateId, IState<TContext, TStateId>> states,
            TStateId initialState,
            List<StateTransition<TContext, TStateId>> transitions = null
        ){
            States = states;
            Transitions = transitions ?? new List<StateTransition<TContext,TStateId>>();
            InitialState = initialState;
        }
    }
}