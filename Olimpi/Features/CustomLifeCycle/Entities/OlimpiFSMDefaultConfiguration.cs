using System.Collections.Generic;
using Olimpi.Core.Domain.Entities;
using Olimpi.Core.Domain.Interfaces;
using Olimpi.Features.CustomLifeCycle.Entities.States;

namespace Olimpi.Features.CustomLifeCycle.Entities
{
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
                new(OlimpiStateId.Awake, OlimpiStateId.OnEnable, _ => true),
                new(OlimpiStateId.OnEnable, OlimpiStateId.Reset, _ => true),
                new(OlimpiStateId.Reset, OlimpiStateId.Start, _ => true),
                new(OlimpiStateId.Start, OlimpiStateId.FixedUpdate, _ => true),
                new(OlimpiStateId.FixedUpdate, OlimpiStateId.Update, _ => true),
                new(OlimpiStateId.Update, OlimpiStateId.LateUpdate, _ => true),
                new(OlimpiStateId.LateUpdate, OlimpiStateId.OnApplicationPause, _ => true),
                new(OlimpiStateId.OnApplicationPause, OlimpiStateId.FixedUpdate, _ => true),

                new(OlimpiStateId.OnApplicationPause, OlimpiStateId.OnApplicationQuit, _ => false),
                new(OlimpiStateId.OnApplicationQuit, OlimpiStateId.OnDisable, _ => true),
                new(OlimpiStateId.OnDisable, OlimpiStateId.OnDestroy, _ => true),

                new(OlimpiStateId.OnDestroy, OlimpiStateId.Awake, _=> false),
            }
        )
        { }
    }
}