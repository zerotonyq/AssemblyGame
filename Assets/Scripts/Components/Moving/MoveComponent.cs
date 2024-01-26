using System;
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
        
        private Vector3 _currentMoveDirection;
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
            _currentMoveDirection = new Vector3(direction.x, 0, direction.y);
        }

        public void SetDirectionFromPosition(Vector3 pos)
        {
            _currentMoveDirection = Vector3.ClampMagnitude(new Vector3(pos.x, 0, pos.z), 1);
        }
        private void FixedUpdate()
        {
            _currentVelocity = _rigidbody.velocity;

            var planeVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);
            var fallVelocity = new Vector3(0, _currentVelocity.y, 0);

            if (Vector3.SqrMagnitude(planeVelocity) >= _maximumWalkSpeed)
            {
                planeVelocity = Vector3.ClampMagnitude(planeVelocity, _maximumWalkSpeed);
            }
            
            if (Vector3.SqrMagnitude(fallVelocity) >= _maximumFallSpeed)
            {
                fallVelocity = Vector3.ClampMagnitude(fallVelocity, _maximumFallSpeed);
            }
            
            _rigidbody.velocity = planeVelocity + fallVelocity;
            
            _rigidbody.AddForce(_currentMoveDirection * _accelerationRate);

        }

        

        public Vector2 CurrentMoveDirection => _currentMoveDirection;
        public bool IsMoving => _isMoving;
        public float AccelerationRate => _accelerationRate;
        public float MaximumWalkSpeed => _maximumWalkSpeed;
        public float MaximumFallSpeed => _maximumFallSpeed;

    }
}
