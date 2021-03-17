namespace RoS.Gameplay.Entities
{   
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.AI;

    using RoS.Camera;
    using RoS.Gameplay.Equipment;
    using RoS.HUD;
    
    /// <summary>
    /// This class holds the controls for the player and all its possible interactions.
    /// For example, its will hold the current entity controled and access all the possible actions with that entity.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("INVENTORIES")]
        public Equipment equipment;
        [Space()]
        public GameObject startBackpack;
        public GameObject startRSystem;

        [Header("POINT Of INTEREST")]
        public Transform pointOfInterest;
        public float poiMaxDistanceCheck;
        public float poiSearchRadius;

        [Header("LOCAL REFERENCES")]
        private PlayableEntity playableEntity;

        private void Awake() {
            // Local References
            playableEntity = transform.GetComponentInChildren<PlayableEntity>();
            
            // HUD
            if (GameObject.Find("HUDManager")) {
                CoreInfo info = playableEntity.info;
                Stats stats = playableEntity.stats;
                HUDManager.SetName(info.name);
                HUDManager.SetTitle(info.title);
                HUDManager.SetBars(stats.maxHealth, stats.health, stats.maxMana, stats.mana);
            }

            // Events
            GameObject.Find("Player Input").GetComponent<PlayerInput>().onActionTriggered += HandleAction;

            // Equipment
            equipment.Equip(startBackpack);
            equipment.Equip(startRSystem);
        }

        private void Update() {
            
        }

        private void HandleAction(InputAction.CallbackContext ctx) {
            // Interact (move, action, object interaction, etc...) >> Left mouse button (0)
            if (ctx.action.name == "Interact") {
                Interact();
            }
        }

        protected virtual void Interact() {
            // We need to make sure that no modal is open in order to do any action in the world
            if (GameManager.openModals.Count == 0) {
                // We cast a ray
                Ray r = CameraDirector.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(r, out hit)) {
                    // In case we hit the ground/terrain, we move to the clicked location
                    if (hit.collider.gameObject.tag == "Terrain" && playableEntity.navMeshAgent.isOnNavMesh) {
                        playableEntity.navMeshAgent.SetDestination(hit.point);
                        
                    }

                    // In case we hit an NPC, we start an interaction with the NPC
                    if (hit.collider.gameObject.tag == "NPC") {
                        hit.collider.gameObject.GetComponent<NPC>().OnSelection();
                    }

                    // In case we hit an entity, we start an interaction with the entity
                    if (hit.collider.gameObject.tag == "Entity") {
                        // CODE HERE...
                        // ==> Start battle
                        
                    }
                }
            }
        }

        #region NAVMESH
        private bool IsMoving() {
            NavMeshAgent agent = playableEntity.navMeshAgent;
            return (agent.hasPath && agent.remainingDistance > 0) ? true : false;
        }
        private bool HasArrivedToDestination() {
            NavMeshAgent agent = playableEntity.navMeshAgent;
            return (agent.hasPath && agent.remainingDistance < 0.1f) ? true : false;
        }
        #endregion
        
        #region MISC
        private void OnDrawGizmosSelected() {
            Gizmos.color = ExtendedColor.RGB(129, 198, 247);
            Gizmos.DrawRay(transform.position, transform.forward * poiMaxDistanceCheck);
            Gizmos.color = ExtendedColor.RGB(186, 139, 247);
            Gizmos.DrawWireSphere(transform.position + transform.forward * poiMaxDistanceCheck, poiSearchRadius);
        }
        #endregion
    }
}