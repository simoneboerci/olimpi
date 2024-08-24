using StateManagementSystem.Core.Domain.Interfaces;
using StateManagementSystem.Core.Domain.ValueObjects;

namespace StateManagementSystem.Core.Domain.Entities
{
    public class StateMachine<TContext, TStateId>(
        TContext context,
        IDictionary<TStateId, IState<TContext, TStateId>>? states = null,
        IList<StateTransition<TContext, TStateId>>? transitions = null
        ) : IStateMachine<TContext, TStateId> where TContext : IStateMachineContext where TStateId : Enum
    {
        private readonly TContext _context = context;

        private readonly IDictionary<TStateId, IState<TContext, TStateId>> _states = states ?? new Dictionary<TStateId, IState<TContext, TStateId>>();
        private readonly IList<StateTransition<TContext, TStateId>> _transitions = transitions ?? new List<StateTransition<TContext, TStateId>>();

        private IState<TContext, TStateId>? _currentState;
        private TStateId? _currentStateId;

        public TContext Context => _context;
        public TStateId? CurrentStateId => _currentStateId;

        public bool HasContext => _context != null;
        public bool HasStates => _states != null && _states.Count > 0;
        public bool HasTransitions => _transitions != null && _transitions.Count > 0;

        public event Action<TStateId?, TStateId>? StateChanged;

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