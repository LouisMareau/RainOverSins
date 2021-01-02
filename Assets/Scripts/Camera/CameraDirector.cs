namespace RoS.Camera
{
    using UnityEngine;
    using RoS.Camera.Utils;

    // Entities import
    using RoS.Gameplay.Entities;

    public class CameraDirector : MonoBehaviour
    {
        public static Camera main {
            get { return Camera.main; }
        }

        [Header("CORE")]
        public Utils.CameraType cameraType;

        [Header("TACTICAL MODE")]
        public Vector3 tacticalBaseOffset;
        public Vector3 tacticalBaseRotation;

        [Header("CINEMATIC MODE")]
        public Vector3 cinematicBaseOffset;
        public Vector3 cinematicBaseRotation;

        [Header("REFERENCES")]
        public Player player;

        private Transform playerT; // A reference to the player's transform (Character.Player.transform)
        private Transform playerPOI; // A reference to the player's Point of Interest (Character.Player.pointOfInterest) 

        public Vector3 offset; // The offset of the camera (should be placed somewhere behind the player that doesn't obstruct visibility and offers a dynamic cinematic POV)

        private void Start() {
            playerT = player.transform;
            playerPOI = player.pointOfInterest;
        }

        private void Update() {
            // The camera should be looking at the player's point of interest
            playerPOI = player.pointOfInterest;

            if (playerPOI != null) {
                this.transform.LookAt(playerPOI);
            }
        }

        #region BEHAVIOUR
        public void Define(Transform target) {
            switch (cameraType) {
                // Cinematic camera behaviour (Third-Person view)
                case Utils.CameraType.CINEMATIC:
                    // Setup the camera's position and basic rotation
                break;
                // Tactical camera behaviour (Top-Down view)
                case Utils.CameraType.TACTICAL:
                    // Setup the camera's position and basic rotation

                break;
            }
        }
        #endregion
    }
}