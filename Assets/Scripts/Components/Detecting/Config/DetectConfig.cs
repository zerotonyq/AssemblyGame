using System;

namespace Game.Components.Config
{
    public class DetectConfig
    {
        private Type _targetType;
        private const float DEFAULT_DETECTION_DELAY = 0.3f;
        private const float DEFAULT_DETECTION_RADIUS = 4f;
        private const int DEFAULT_DETECTION_LAYER = 6;
        private const int DEFAULT_DETECTION_OBJECTS_COUNT = 5;
        private const float DEFAULT_DETECTION_DISTANCE = 5;
        
        private float _detectionDelay;
        private float _detectionRadius;
        private int _detectionLayer;
        private int _detectionObjectsCount;
        private float _detectionDistance;

        public DetectConfig(
            Type targetType,
            float detectionDelay = DEFAULT_DETECTION_DELAY, 
            float detectionRadius = DEFAULT_DETECTION_RADIUS, 
            int detectionLayer = DEFAULT_DETECTION_LAYER, 
            int detectionObjectsCount = DEFAULT_DETECTION_OBJECTS_COUNT, 
            float detectionDistance = DEFAULT_DETECTION_DISTANCE)
        {
            _targetType = targetType ?? typeof(PlayerComponent);
            _detectionDelay = detectionDelay;
            _detectionRadius = detectionRadius;
            _detectionLayer = detectionLayer;
            _detectionObjectsCount = detectionObjectsCount;
            _detectionDistance = detectionDistance;
        }
        
        public Type TargetType => _targetType;

        public float DetectionDelay => _detectionDelay;

        public float DetectionRadius => _detectionRadius;

        public int DetectionLayer => _detectionLayer;

        public int DetectionObjectsCount => _detectionObjectsCount;

        public float DetectionDistance => _detectionDistance;
        
        public static DetectConfig DefaultConfig = new DetectConfig(typeof(PlayerComponent));
    }
}