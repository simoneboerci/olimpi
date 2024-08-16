using System;
using System.Diagnostics.CodeAnalysis;
using Olimpi.features.custom_life_cycle.domain.interfaces;

namespace Olimpi.features.custom_life_cycle.domain.entities{
    public readonly struct StateTransition<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        public TStateId FromState{get;}
        public TStateId ToState{get;}
        public Func<TContext, bool> Condition{get;}

        public StateTransition(TStateId fromState, TStateId toState, Func<TContext, bool> condition){
            FromState = fromState;
            ToState = toState;
            Condition = condition;
        }
    }
}