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
        public int turnsSinceDeath;

        protected virtual void OnValidate() {
            stats.UpdateStats();
        }

        protected virtual void Awake() {
            collider = GetComponent<Collider>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Update() {
            // Move
            if (Input.GetMouseButton(0)) {
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
            }
            
            // Actions (skills)
            // Interactions
            // Misc
        }

        protected virtual void Move(RaycastHit hit) {
            navMeshAgent.SetDestination(hit.point);
        }
    }
}