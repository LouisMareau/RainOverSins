namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    
    using RoS.Gameplay.Equipment;
    
    public class PlayableEntity : Entity
    {
        public enum BattleState {
            ALIVE,
            DEAD_RECENTLY,
            DEAD_LATE
        }

        [Header("PHYSICS")]
        private new Collider collider;
        [HideInInspector] public NavMeshAgent navMeshAgent;

        [Header("BATTLE")]
        public BattleState battleState;

        /// <summary>
        /// Amount of turns (of the ongoing battle) the entity has fainted.
        /// </summary>
        public int turnsSinceFainted;
        /// <summary>
        /// Amount of turns (of the ongoing battle) the entity has been dead.
        /// </summary>
        public int turnsSinceDeath;
        /// <summary>
        /// Once the entity's HP reaches 0 (zero) for the first time, it faints. This allows the player to try and revive the entity within the next 3 tuns, at which time, the creature will die for good.
        /// </summary>
        public bool isFainted;
        /// <summary>
        /// If the entity's HP has reached 0 (zero) and has been in the faint state for more than 3 turns, the entity is considered dead. 
        /// Only the item "Pheonix Ashes" is able to revive a dead entity.
        /// The previously cited item being extremely rare and , the player will be able to bury the fallen entities. 
        /// </summary>
        public bool isDead;

        protected override void OnValidate() {
            base.OnValidate();

            stats.SetStats();
        }

        protected virtual void Awake() {
            collider = transform.Find("Mesh").GetComponent<Collider>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            // ** This needs to be set from last save when first starting the game and saved before exiting the game
            stats.health = stats.maxHealth;
            stats.mana = stats.maxMana;
        }

        protected virtual void Update() {            
            // Actions (skills)
            // Interactions
            // Misc
        }

       
    }
}