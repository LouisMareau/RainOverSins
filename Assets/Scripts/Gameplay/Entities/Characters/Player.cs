namespace RoS.Gameplay.Entities
{   
    using UnityEngine;

    using RoS.Gameplay.Equipment;
    using RoS.HUD;
    
    public class Player : PlayableEntity
    {
        [Header("INVENTORY")]
        public Equipment equipment;

        [Header("POINT Of INTEREST")]
        public Transform pointOfInterest;
        public float poiMaxDistanceCheck;
        public float poiSearchRadius;

        #region LOCAL REFERENCES
        [HideInInspector] public Transform storagesT;
        #endregion

        protected override void Awake() {
            base.Awake();

            // Local References
            storagesT = transform.Find("Storages");

            // ** This needs to be set from last save when first starting the game and saved before exiting the game
            stats.health = stats.maxHealth;
            stats.mana = stats.maxMana;

            // HUD
            HUDManager.SetName(info.name);
            HUDManager.SetTitle(info.title);
            HUDManager.SetBars(stats.maxHealth, stats.health, stats.maxMana, stats.mana);
        }

        protected override void Update() {
            base.Update();
            
            // Use stamina while moving during a battle

            // We look for points of interest around the player sight
            LookForPointsOfInterest();
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