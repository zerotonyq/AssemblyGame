namespace Game.Entities.Enemy
{
    public class NavMeshConfig
    {

        private const float DEFAULT_STOPPING_DISTANCE = 1.5f;
        private const float DEFAULT_ACCELERATION = 30f;
        private const bool DEFAULT_UPDATE_POSITION = false;
        private const bool DEFAULT_UPDATE_ROTATION = false;
        
        private float _stoppingDistance;
        private float _acceleration;
        private bool _updatePosition;
        private bool _updateRotation;
        
        public NavMeshConfig(
            float acceleration = DEFAULT_ACCELERATION, 
            float stopDist = DEFAULT_STOPPING_DISTANCE, 
            bool updatePosition = DEFAULT_UPDATE_POSITION, 
            bool updateRotation = DEFAULT_UPDATE_ROTATION)
        {
            _acceleration = acceleration;
            _stoppingDistance = stopDist;
            _updatePosition = updatePosition;
            _updateRotation = updateRotation;
        }
        
        public float Acceleration => _acceleration;
        
        public float StoppingDistance => _stoppingDistance;

        public bool UpdatePosition => _updatePosition;

        public bool UpdateRotation => _updateRotation;
        
        public static NavMeshConfig defaultConfig = new NavMeshConfig();
    }
}