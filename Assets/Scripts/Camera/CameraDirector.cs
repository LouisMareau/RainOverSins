namespace RoS.Camera
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    using RoS.Gameplay.Entities;
    using RoS.Camera.Utils;

    public class CameraDirector : MonoBehaviour
    {
        public static Camera main {
            get { return Camera.main; }
        }

        [Header("CORE")]
        public Utils.CameraType type;
        public CameraMovementState movementState;

        [Header("MOVEMENT (booleans)")]
        public Vector3 maxVelocity;
        private Vector3 velocity;
        [Space()]
        public float accelerationTime = 1.5f;
        public float decelerationTime = 0.5f;
        private bool isLeft = false;
        private bool isRight = false;
        private bool isUp = false;
        private bool isDown = false;

        #region LOCAL REFS
        private Player player;
        private Transform playerT; // A reference to the player's transform (Character.Player.transform)
        private Transform playerPOI; // A reference to the player's Point of Interest (Character.Player.pointOfInterest) 
        #endregion
        
        private void Awake() {
            player = GameObject.Find("Player").GetComponent<Player>();
            GameObject.Find("Player Input").GetComponent<PlayerInput>().onActionTriggered += HandleAction;
        }

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

            // Movement
            Move();
        }

        private void HandleAction(InputAction.CallbackContext ctx) {
            // Moves the Camera >> ZQSD keys
            if (ctx.action.name == "MoveCamera") {
                DefineMovement(ctx.ReadValue<Vector2>());
            }
        }

        #region BEHAVIOUR
        public void DefineMovement(Vector2 direction) {
            if (direction.x == 0) {
                isRight = false;
                isLeft = false;
            }
            if (direction.x > 0) { isRight = true; }
            if (direction.x < 0) { isLeft = true; }
            if (direction.y == 0) {
                isUp = false;
                isDown = false;
            }
            if (direction.y > 0) { isUp = true; }
            if (direction.y < 0) { isDown = true; }
        }

        public void Move() {
            // We define the velocity depending on the axis' sign
            SetVelocity();
            // We translate the position of the camera to the new coordinates 
            this.transform.Translate(velocity.x * Time.deltaTime, 0, velocity.z * Time.deltaTime, Space.Self);
        }

        private void SetVelocity() {
            if (isRight) { velocity.x += (maxVelocity.x / accelerationTime) * Time.deltaTime; }
            if (isLeft) { velocity.x -= (maxVelocity.x / accelerationTime) * Time.deltaTime; }
            if (isUp) { velocity.z += (maxVelocity.z / accelerationTime) * Time.deltaTime; }
            if (isDown) { velocity.z -= (maxVelocity.z / accelerationTime) * Time.deltaTime; }
            velocity.x = Mathf.Clamp(velocity.x, -maxVelocity.x, maxVelocity.x);
            velocity.z = Mathf.Clamp(velocity.z, -maxVelocity.z, maxVelocity.z);

            if (!isRight && !isLeft && velocity.x > 0) { velocity.x -= (maxVelocity.x / decelerationTime) * Time.deltaTime; }
            if (!isRight && !isLeft && velocity.x < 0) { velocity.x += (maxVelocity.x / decelerationTime) * Time.deltaTime; }
            if (!isUp && !isDown && velocity.z > 0) { velocity.z -= (maxVelocity.z / decelerationTime) * Time.deltaTime; }
            if (!isUp && !isDown && velocity.z < 0) { velocity.z += (maxVelocity.z / decelerationTime) * Time.deltaTime; }
            // We reset the velocity to 0 (zero) if no input is pressed
            if (!isUp && !isDown && !isRight && !isLeft) {
                if (velocity.x > 0) { 
                    velocity.x -= (maxVelocity.x / decelerationTime) * Time.deltaTime; 
                    if (velocity.x < 0) { velocity.x = 0; }
                }
                if (velocity.x < 0) { 
                    velocity.x += (maxVelocity.x / decelerationTime) * Time.deltaTime; 
                    if (velocity.x > 0) { velocity.x = 0; }
                }
                if (velocity.z > 0) { 
                    velocity.z -= (maxVelocity.z / decelerationTime) * Time.deltaTime; 
                    if (velocity.z < 0) { velocity.z = 0; }
                }
                if (velocity.z < 0) { 
                    velocity.z += (maxVelocity.z / decelerationTime) * Time.deltaTime;
                    if (velocity.z > 0) { velocity.z = 0; } 
                }
            }
        }
        #endregion
    }
}