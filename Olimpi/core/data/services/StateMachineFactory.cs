using System;
using Olimpi.Core.Domain.Entities;
using Olimpi.Core.Domain.Interfaces;

namespace Olimpi.Core.Data.Services
{
    public class StateMachineFactory
    {
        public static IStateMachine<TContext, TStateId> Create<TContext, TStateId>() where TContext : IStateMachineContext where TStateId : Enum
        {
            // Crea un nuovo contesto per la state machine
            var context = Activator.CreateInstance<TContext>();
            // Ritorna la nuova state machine passando il contesto come parametro
            return new StateMachine<TContext, TStateId>(context);
        }
    }
}