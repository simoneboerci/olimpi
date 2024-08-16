using cAlgo.API;
using Olimpi.core.data.services;
using Olimpi.core.domain.entities;
using Olimpi.core.domain.interfaces;
using Olimpi.features.custom_life_cycle.entities;

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