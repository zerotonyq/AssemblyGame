namespace MoveSystem
{
    public class MoveConfig
    {
        private const float DEFAULT_MAXIMUM_WALK_SPEED = 3.5f;
        private const float DEFAULT_MAXIMUM_FALL_SPEED = 10f;
        private const float DEFAULT_ACCELERATION_RATE = 5000f;
        public MoveConfig(
            float maximumWalkSpeed = DEFAULT_MAXIMUM_WALK_SPEED, 
            float maximumFallSpeed = DEFAULT_MAXIMUM_FALL_SPEED, 
            float accelerationRate = DEFAULT_ACCELERATION_RATE)
        {
            MaximumWalkSpeed = maximumWalkSpeed;
            MaximumFallSpeed = maximumFallSpeed;
            AccelerationRate = accelerationRate;
        }

        public float MaximumWalkSpeed { get; }
        public float MaximumFallSpeed { get; }
        public float AccelerationRate { get; }
        
        public static MoveConfig DefaultMoveConfig = new MoveConfig();
    }
    
    
}