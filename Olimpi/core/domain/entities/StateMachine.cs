using System;
using System.Collections.Generic;
using System.Linq;
using Olimpi.core.domain.interfaces;

namespace Olimpi.core.domain.entities
{
    public class StateMachine<TContext, TStateId> : IStateMachine<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        private readonly TContext _context;

        private readonly Dictionary<TStateId, IState<TContext, TStateId>> _states = new();
        private readonly List<StateTransition<TContext, TStateId>> _transitions = new();

        private IState<TContext, TStateId> _currentState;
        private TStateId _currentStateId;

        public event Action<TStateId, TStateId> StateChanged;

        public StateMachine(
            TContext context,
            Dictionary<TStateId, IState<TContext, TStateId>> states = null,
            List<StateTransition<TContext, TStateId>> transitions = null
        )
        {
            _context = context;
            _states = states ?? new();
            _transitions = transitions ?? new();
        }

        public void AddState(TStateId stateId, IState<TContext, TStateId> state)
        {
            if (!_states.ContainsKey(stateId)) _states[stateId] = state;
            else throw new Exception(); //TODO: Lanciare un'eccezione
        }

        public void AddTransition(StateTransition<TContext, TStateId> transition)
        {
            if (!_transitions.Contains(transition)) _transitions.Add(transition);
            else throw new Exception(); //TODO: Lanciare un'eccezione
        }

        public void ChangeState(TStateId stateId)
        {
            // Se lo stato non è presente nella lista degli stati lancia un'eccezione
            if (!_states.ContainsKey(stateId)) throw new ArgumentException($"State {stateId} not found.");

            // Tieni traccia dello stato corrente
            var oldStateId = _currentStateId;

            // Esci dallo stato corrente se presente
            _currentState?.Exit(_context);

            // Aggiorna l'id dello stato corrente
            _currentStateId = stateId;

            // Aggiorna lo stato corrente
            _currentState = _states[stateId];

            // Entra nello stato corrente
            _currentState.Enter(_context);

            // Invoca la callback del cambio di stato avvenutow
            StateChanged?.Invoke(oldStateId, _currentStateId);
        }

        public void Update()
        {
            // Aggiorna lo stato corrente se disponibile
            _currentState?.Update(_context);

            // Per ogni transizione nella lista di transizioni che ha come punto di partenza lo stato corrente
            foreach (var transition in _transitions.Where(t => t.FromState.Equals(_currentStateId)))
            {
                // Se la condizione della transizione si verifica
                if (transition.Condition(_context))
                {
                    // Passa allo stato di arrivo definito dalla transizione
                    ChangeState(transition.ToState);
                    // Esci dal loop
                    break;
                }
            }
        }
    }
}