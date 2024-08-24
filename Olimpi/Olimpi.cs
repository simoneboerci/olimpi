using cAlgo.API;
using Olimpi.Core.Data.Services;
using Olimpi.Core.Domain.Entities;
using Olimpi.Core.Domain.Interfaces;
using Olimpi.Features.CustomLifeCycle.Entities;

namespace Olimpi
{
    [Robot(AccessRights = AccessRights.FullAccess, AddIndicators = true)]
    public class Olimpi : Robot
    {   
        private IStateMachine<OlimpiContext, OlimpiStateId> _stateMachine;
        private StateMachineConfigurationBase<OlimpiContext, OlimpiStateId> _stateMachineConfiguration;

        protected override void OnStart() => InitializeStateMachine();

        protected override void OnTick() => _stateMachine.Update();

        private void InitializeStateMachine()
        {
            _stateMachine = StateMachineFactory.Create<OlimpiContext, OlimpiStateId>();
            StateMachineConfigurator.Configure(_stateMachine, new OlimpiFSMDefaultConfiguration());
        }
    }
}