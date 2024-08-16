using Olimpi.core.domain.interfaces;

namespace Olimpi.features.custom_life_cycle.entities {
    public class OlimpiContext : IStateMachineContext { 
        public bool IsRunning { get; set; } 
        public int ErrorCount { get; set; }
    }
}