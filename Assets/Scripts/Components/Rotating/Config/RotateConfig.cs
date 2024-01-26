namespace Game.Components.Rotating.Config
{
    public class RotateConfig
    {
        private const float DEFAULT_ROTATION_MAX_SPEED = 0.1f;
        private const float DEFAULT_ROTATION_END_DELTA_DEGREES = 5f;
        private const bool DEFAULT_AUTO_ROTATION_TO_MOVE_DIRECTION = false;
        public RotateConfig(
            float rotationMaxSpeed = DEFAULT_ROTATION_MAX_SPEED,
            float rotationEndDeltaDegrees = DEFAULT_ROTATION_END_DELTA_DEGREES,
            bool autoRotateToMoveDirection = DEFAULT_AUTO_ROTATION_TO_MOVE_DIRECTION)
        {
            RotationMaxSpeed = rotationMaxSpeed;
            RotationEndDeltaDegrees = rotationEndDeltaDegrees;
            AutoRotateToMoveDirection = autoRotateToMoveDirection;
        }
        
        public float RotationMaxSpeed { get; }
        public float RotationEndDeltaDegrees { get; }
        
        public bool AutoRotateToMoveDirection { get; }

        public static RotateConfig DefaultRotateConfig = new RotateConfig();
    }
}