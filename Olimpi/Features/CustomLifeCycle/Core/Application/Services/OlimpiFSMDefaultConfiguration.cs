using System.Collections.Generic;
using Olimpi.Features.CustomLifeCycle.Core.Domain.Entities;
using Olimpi.Features.CustomLifeCycle.Core.Domain.Enums;
using Olimpi.Features.CustomLifeCycle.Entities.States;
using StateManagementSystem.Core.Domain.Entities;
using StateManagementSystem.Core.Domain.Interfaces;

namespace Olimpi.Features.CustomLifeCycle.Core.Application.Services
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

            [
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
            ]
        )
        { }
    }
}