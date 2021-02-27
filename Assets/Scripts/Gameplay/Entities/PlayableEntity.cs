namespace RoS.Gameplay.Entities
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    
    using RoS.Camera;
    
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
        private NavMeshAgent navMeshAgent;

        [Header("BATTLE")]
        public BattleState battleState;

        [Header("DEATH-RELATED BATTLE MEMBERS")]
        [Tooltip("")]
        public int turnsSinceFainted;
        /// <summary>
        /// Amount of turns (of the ongoing battle) the entity has been dead.
        /// </summary>
        public int turnsSinceDeath;
        /// <summary>
        /// Once the entity's HP reaches 0 (zero) for the first time, it faints. This allows the player to try and revive the entity within the next 3 tuns, at which time, the creature will die for good
        /// </summary>
        public bool isFainted;
        /// <summary>
        /// If the entity's HP has reached 0 (zero) and has been in the faint state for more than 3 turns, the entity is considered dead. 
        /// Only the item "Pheonix Ashes" is able to revive a dead entity.
        /// The previously cited item being extremely rare and , the player will be able to bury the fallen entities. 
        /// </summary>
        public bool isDead;

        protected virtual void OnValidate() {
            stats.SetStats();
        }

        protected virtual void Awake() {
            collider = GetComponent<Collider>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Update() {
            // Move
            /*if (Input.GetMouseButton(0)) {
                // We need to make sure that no modal is open in order to do any action in the world
                if (GameManager.openModals.Count == 0) {
                    // We cast a ray
                    Ray r = CameraDirector.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(r, out hit)) {
                        // In case we hit the ground/terrain, we move to the clicked location
                        if (hit.collider.gameObject.tag == "Terrain" && navMeshAgent.isOnNavMesh) {
                            Move(hit);
                        }

                        // In case we hit an NPC, we start an interaction with the NPC
                        if (hit.collider.gameObject.tag == "NPC") {
                            hit.collider.gameObject.GetComponent<NPC>().OnSelection();
                        }
                    }
                }
            }*/
            
            // Actions (skills)
            // Interactions
            // Misc
        }

        protected virtual void Move(RaycastHit hit) {
            navMeshAgent.SetDestination(hit.point);
        }
    }
}