namespace RoS.Gameplay.Entities
{   
    using UnityEngine;
    using UnityEngine.AI;

    using RoS.Camera;
    using RoS.Gameplay.Resources;
    using RoS.Gameplay.Storages;
    
    public class Player : PlayableEntity
    {
        [Header("STORAGES")]
        public GameObject backpack;
        public GameObject cSystem;

        [Header("POINT Of INTEREST")]
        public Transform pointOfInterest;
        public float poiMaxDistanceCheck;
        public float poiSearchRadius;

        #region Local References
        [HideInInspector] public Transform storagesT;
        private GameObject modals;
        private NavMeshAgent navMeshAgent;
        #endregion

        private void Awake() {
            // Local References
            modals = GameObject.Find("Modals");
            navMeshAgent = GetComponent<NavMeshAgent>();
            storagesT = transform.Find("Storages");
        }

        

        private void Update() {
            if (Input.GetMouseButton(0)) {
                Ray r = CameraDirector.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(r, out hit)) {
                    if (modals.transform.childCount == 0) {
                        if (hit.collider.gameObject.tag == "Terrain" && navMeshAgent.isOnNavMesh) {
                            Move(hit);
                        }
                        if (hit.collider.gameObject.tag == "NPC") {
                            hit.collider.gameObject.GetComponent<NPC>().OnSelection();
                        }
                    }
                }
            }

            // Use stamina while moving during a battle

            // We look for points of interest around the player sight
            LookForPointsOfInterest();
        }

        #region MOVEMENT
        public void Move(RaycastHit hit) {
            navMeshAgent.SetDestination(hit.point);
        }
        #endregion
        
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