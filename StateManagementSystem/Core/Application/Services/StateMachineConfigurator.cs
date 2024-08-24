using StateManagementSystem.Core.Domain.Entities;
using StateManagementSystem.Core.Domain.Interfaces;

namespace StateManagementSystem.Core.Application.Services
{
    public class StateMachineConfigurator
    {
        public static void Configure<TContext, TStateId>(IStateMachine<TContext, TStateId> stateMachine, StateMachineConfigurationBase<TContext, TStateId> configuration) where TContext : IStateMachineContext where TStateId : Enum
        {
            foreach (var state in configuration.States)
            {
                stateMachine.AddState(state.Key, state.Value);
            }

            foreach (var transition in configuration.Transitions)
            {
                stateMachine.AddTransition(transition);
            }

            stateMachine.ChangeState(configuration.InitialState);
        }
    }
}