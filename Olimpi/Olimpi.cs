using cAlgo.API;
using Olimpi.features.custom_life_cycle.domain.application.services;
using Olimpi.features.custom_life_cycle.domain.entities;
using Olimpi.features.custom_life_cycle.domain.interfaces;

namespace Olimpi
{
    [Robot(AccessRights = AccessRights.None, AddIndicators = true)]
    public class Olimpi : Robot
    {    
        private ILifeCycleService _lifeCycleService;

        protected override void OnStart()
        {
            var context = new OlimpiContext();
            var stateMachine = new StateMachine<OlimpiContext>(context);
            
            _lifeCycleService = new OlimpiLifeCycleService(stateMachine, context);

            _lifeCycleService.OnStart();
        }

        protected override void OnTick() => _lifeCycleService.OnTick();
        protected override void OnBar() => _lifeCycleService.OnBar();

        protected override void OnStop() => _lifeCycleService.OnStop();

        protected override void OnError(Error error) => _lifeCycleService.OnError(error.ToString());
    }
}