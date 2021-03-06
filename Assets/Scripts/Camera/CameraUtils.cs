namespace RoS.Camera.Utils
{
    /// <summary>
    ///     CINEMATIC: Offers a third-person camera that always stays behind the player. Cannot be used during combat.
    ///     TACTICAL: Top-down camera that offers an instant view of the surroundings. Can be used either during combat or outside.
    /// </summary>
    public enum CameraType {
        CINEMATIC,
        TACTICAL
    }

    public enum CameraMovementState {
        IDLE,
        UP,
        UP_LEFT,
        UP_RIGHT,
        DOWN,
        DOWN_LEFT,
        DOWN_RIGHT,
        LEFT,
        RIGHT
    }
}