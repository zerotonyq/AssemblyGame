namespace Game.Components.Rotating.Config
{
    public class RotateConfig
    {
        private const float DEFAULT_ROTATION_MAX_SPEED = 5f;
        private const float DEFAULT_ROTATION_END_DELTA_DEGREES = 5f;

        public RotateConfig(
            float rotationMaxSpeed = DEFAULT_ROTATION_MAX_SPEED,
            float rotationEndDeltaDegrees = DEFAULT_ROTATION_END_DELTA_DEGREES)
        {
            RotationMaxSpeed = rotationMaxSpeed;
            RotationEndDeltaDegrees = rotationEndDeltaDegrees;
        }
        
        public float RotationMaxSpeed { get; }
        public float RotationEndDeltaDegrees { get; }

        public static RotateConfig DefaultRotateConfig = new RotateConfig();
    }
}