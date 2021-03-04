namespace RoS.Gameplay.Entities
{   
    using UnityEngine;
    using UnityEngine.InputSystem;

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
            // Use stamina while moving during a battle

            // We look for points of interest around the player sight
            LookForPointsOfInterest();
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
                }
            }
        }
        
        public Transform LookForPointsOfInterest() {
            // First, we check if we are already looking at a point of interest
            if (pointOfInterest != null) {
                if (Vector3.Distance(this.transform.position, pointOfInterest.position) < poiMaxDistanceCheck) {
                    return pointOfInterest;
                }
            }

            // If we didn't find any point of interest in the area, we first trace a line (of sight) in front of the player
            Vector3 lineOfSight = this.transform.position + Vector3.forward * poiMaxDistanceCheck; 
            // Then, at the end of the line of sight, we cast a sphere to check for a point of interest in the area (bit shift on the PoI layer)
            int mask = 1 << 9;
            Collider[] pois = Physics.OverlapSphere(lineOfSight, poiSearchRadius, mask);
            if (pois != null && pois.Length > 0) {
                // If the is a point of interest in the area, we return the point of interest
                pointOfInterest = pois[0].transform;
            }

            // Otherwise, we return a null object
            return null;
        }
        
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