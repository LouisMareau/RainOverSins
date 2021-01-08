namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PlayableEntity : Entity
    {
        public enum State {
            ALIVE,
            DEAD_RECENTLY,
            DEAD_LATE
        }

        [Header("ATTRIBUTES")]
        public Stats stats;

        [Header("STATUSES")]
        public List<Status> statuses;

        [Header("STATE")]
        public State state;
        public int turnsSinceDeath;

        private void OnValidate() {
            stats.UpdateStats();
        }
    }
}