namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PlayableEntity : Entity
    {
        public enum BattleState {
            ALIVE,
            DEAD_RECENTLY,
            DEAD_LATE
        }

        [Header("ATTRIBUTES")]
        public Stats stats;

        [Header("STATUSES")]
        public List<Status> statuses;

        [Header("PHYSICS")]
        private new Collider collider;

        [Header("BATTLE")]
        public BattleState battleState;
        public int turnsSinceDeath;

        private void OnValidate() {
            stats.UpdateStats();
        }

        private void Awake() {
            collider = GetComponent<Collider>();
        }
    }
}