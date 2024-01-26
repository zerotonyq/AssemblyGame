using UnityEngine;

namespace MoveSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveComponent : MonoBehaviour, IMovable
    {
        private float _accelerationRate = 0f;
        private float _maximumWalkSpeed = 0f;
        private float _maximumFallSpeed = 0f;
        private Rigidbody _rigidbody;
        
        private bool _isMoving = false;
        
        private Vector2 _currentMoveDirection;
        private Vector3 _currentVelocity;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Init(MoveConfig settings)
        {
            _accelerationRate = settings.AccelerationRate;
            _maximumWalkSpeed = settings.MaximumWalkSpeed;
            _maximumFallSpeed = settings.MaximumFallSpeed;
        }
        
        public void SetDirectionFromInput(Vector2 direction)
        {
            _currentMoveDirection = direction.normalized;
        }

        public void SetDirectionFromPosition(Vector3 pos)
        {
            _currentMoveDirection = new Vector2(pos.x, pos.z).normalized;
        }
        private void FixedUpdate()
        {
            _currentVelocity = _rigidbody.velocity;
            
            if (Mathf.Abs(_currentVelocity.x) >= _maximumWalkSpeed)
                _currentVelocity = new Vector3(_maximumWalkSpeed * Mathf.Sign(_currentVelocity.x), _currentVelocity.y, _currentVelocity.z);
            
            if (Mathf.Abs(_currentVelocity.y) >= _maximumFallSpeed)
                _currentVelocity = new Vector3(_currentVelocity.x, _maximumFallSpeed * Mathf.Sign(_currentVelocity.y), _currentVelocity.z);
            
            if (Mathf.Abs(_currentVelocity.z) >= _maximumWalkSpeed)
                _currentVelocity = new Vector3(_currentVelocity.x, _currentVelocity.y, _maximumWalkSpeed * Mathf.Sign(_currentVelocity.z));
            
            _rigidbody.velocity = _currentVelocity;
            
            _rigidbody.AddForce(new Vector3(
                _currentMoveDirection.x * _accelerationRate,
                0,
                _currentMoveDirection.y * _accelerationRate));
        }
        public Vector2 CurrentMoveDirection => _currentMoveDirection;
        public bool IsMoving => _isMoving;
        public float AccelerationRate => _accelerationRate;
        public float MaximumWalkSpeed => _maximumWalkSpeed;
        public float MaximumFallSpeed => _maximumFallSpeed;

    }
}
