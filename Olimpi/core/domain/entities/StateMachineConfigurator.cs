using System;
using Olimpi.core.domain.interfaces;

namespace Olimpi.core.domain.entities{
    public class StateMachineConfigurator {
        public static void Configure<TContext, TStateId>(IStateMachine<TContext, TStateId> stateMachine, StateMachineConfigurationBase<TContext, TStateId> configuration) where TContext : IStateMachineContext where TStateId : Enum{
            foreach(var state in configuration.States){
                stateMachine.AddState(state.Key, state.Value);
            }

            foreach(var transition in configuration.Transitions){
                stateMachine.AddTransition(transition);
            }

            stateMachine.ChangeState(configuration.InitialState);
        }
    }
}