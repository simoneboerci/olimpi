using StateManagementSystem.Core.Application.Services;
using StateManagementSystem.Core.Domain.Entities;
using StateManagementSystem.Core.Domain.Interfaces;

namespace StateManagementSystem.Tests;

public class StateMachineTests
{
    private IStateMachine<StateMachineContext, StateId>? _stateMachine;

    [Fact]
    public void Create_StateMachine(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        Assert.NotNull(_stateMachine);
    }

    [Fact]
    public void Configure_StateMachine(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        Assert.True(_stateMachine.HasContext);
        Assert.True(_stateMachine.HasStates);
        Assert.True(_stateMachine.HasTransitions);
    }

    [Fact]
    public void Manual_ChangeState(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        _stateMachine.ChangeState(StateId.C);
        Assert.Equal(StateId.C, _stateMachine.CurrentStateId);
    }

    [Fact]
    public void ChangeState_With_Transition(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        _stateMachine.Update();
        Assert.Equal(StateId.B, _stateMachine.CurrentStateId);
    }

    [Fact]
    public void Enter_CurrentState(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        _stateMachine.ChangeState(StateId.A);
        Assert.True(_stateMachine.Context.StateEntered);
    }

    [Fact]
    public void Update_CurrentState(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        _stateMachine.ChangeState(StateId.A);
        _stateMachine.Update();
        Assert.True(_stateMachine.Context.StateUpdated);
    }

    [Fact]
    public void Exit_CurrentState(){
        _stateMachine = StateMachineFactory.Create<StateMachineContext, StateId>();
        StateMachineConfigurator.Configure(_stateMachine!, new StateMachineConfiguration());

        _stateMachine.ChangeState(StateId.A);
        _stateMachine.ChangeState(StateId.B);
        Assert.True(_stateMachine.Context.StateExited);
    }

    private enum StateId { A, B, C }

    private class StateMachineContext : IStateMachineContext{
        public bool StateEntered = false;
        public bool StateUpdated = false;
        public bool StateExited = false;
    }

    private class StateMachineConfiguration : StateMachineConfigurationBase<StateMachineContext, StateId>
    {
        public StateMachineConfiguration() : base
        (
            new Dictionary<StateId, IState<StateMachineContext, StateId>>{
                { StateId.A, new StateA() },
                { StateId.B, new StateB() },
                { StateId.C, new StateC() }
            },

            StateId.A,

            [
                new(StateId.A, StateId.B, _ => true),
                new(StateId.B, StateId.C, _ => true),
                new(StateId.C, StateId.A, _ => true)
            ]
        ){}
    }

    private class StateA : IState<StateMachineContext, StateId>
    {
        public void Enter(StateMachineContext context) => context.StateEntered = true;
        public void Update(StateMachineContext context) => context.StateUpdated = true;
        public void Exit(StateMachineContext context) => context.StateExited = true;
    }

    private class StateB : IState<StateMachineContext, StateId>
    {
        public void Enter(StateMachineContext context) {}
        public void Update(StateMachineContext context) {}
        public void Exit(StateMachineContext context) {}
    }

    private class StateC : IState<StateMachineContext, StateId>
    {
        public void Enter(StateMachineContext context) {}
        public void Update(StateMachineContext context) {}
        public void Exit(StateMachineContext context) {}
    }
}