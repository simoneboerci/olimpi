using System.Collections.Generic;
using Olimpi.core.domain.entities;
using Olimpi.core.domain.interfaces;
using Olimpi.features.custom_life_cycle.entities.states;

namespace Olimpi.features.custom_life_cycle.entities{
    public class OlimpiFSMDefaultConfiguration : StateMachineConfigurationBase<OlimpiContext, OlimpiStateId>
    {
        public OlimpiFSMDefaultConfiguration() : base
        (
            new Dictionary<OlimpiStateId, IState<OlimpiContext, OlimpiStateId>>
            {
                { OlimpiStateId.Awake, new OlimpiAwakeState() },
                { OlimpiStateId.OnEnable, new OlimpiOnEnableState() },
                { OlimpiStateId.Reset, new OlimpiResetState() },
                { OlimpiStateId.Start, new OlimpiStartState() },
                { OlimpiStateId.FixedUpdate, new OlimpiFixedUpdateState() },
                { OlimpiStateId.Update, new OlimpiUpdateState() },
                { OlimpiStateId.LateUpdate, new OlimpiLateUpdateState() },
                { OlimpiStateId.OnApplicationPause, new OlimpiOnApplicationPauseState() },
                { OlimpiStateId.OnApplicationQuit, new OlimpiOnApplicationQuitState() },
                { OlimpiStateId.OnDisable, new OlimpiOnDisableState() },
                { OlimpiStateId.OnDestroy, new OlimpiOnDestroyState() }
            },

            OlimpiStateId.Awake,

            new List<StateTransition<OlimpiContext, OlimpiStateId>>
            {
                
            }
        ){}
    }
}