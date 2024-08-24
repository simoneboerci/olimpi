using cAlgo.API;
using Olimpi.Features.CustomLifeCycle.Core.Application.Services;
using Olimpi.Features.CustomLifeCycle.Core.Domain.Entities;
using Olimpi.Features.CustomLifeCycle.Core.Domain.Enums;
using StateManagementSystem.Core.Application.Services;
using StateManagementSystem.Core.Domain.Interfaces;

namespace Olimpi
{
    [Robot(AccessRights = AccessRights.FullAccess, AddIndicators = true)]
    public class Olimpi : Robot
    {   
        private IStateMachine<OlimpiContext, OlimpiStateId>? _stateMachine;

        protected override void OnStart() => InitializeStateMachine();

        protected override void OnTick() => _stateMachine!.Update();

        private void InitializeStateMachine()
        {
            _stateMachine = StateMachineFactory.Create<OlimpiContext, OlimpiStateId>();
            StateMachineConfigurator.Configure(_stateMachine, new OlimpiFSMDefaultConfiguration());
        }
    }
}