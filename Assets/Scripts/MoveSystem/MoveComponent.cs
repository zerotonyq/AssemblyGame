using System;
using CommonInterfaces;
using UnityEngine;
using UserInputSystem.InputMoveSystem;
using Zenject;

namespace MoveSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveComponent : MonoBehaviour, IMovable, ISubscribable
    {
        [Inject]
        private InputMoveHandler _inputMoveHandler;
        
        private float _walkSpeed = 0f;
        private float _maximumSpeed = 0f;
        
        private Rigidbody _rigidbody;
        
        private bool _isMoving = false;
        
        private Vector2 _currentMoveDirection;
        private Vector3 _currentVelocity;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Init(10, 100);
        }
        
        public void Init(float walkSpeed, float maximumSpeed)
        {
            _walkSpeed = walkSpeed;
            _maximumSpeed = maximumSpeed;
            Subscribe();
        }
        
        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = new Vector3(
                direction.x * _walkSpeed,
                _currentVelocity.y,
                direction.y * _walkSpeed);
        }
        void FixedUpdate()
        {
            _currentVelocity = _rigidbody.velocity;
            
            if (Mathf.Abs(_currentVelocity.x) >= _maximumSpeed)
                _currentVelocity = new Vector3(_maximumSpeed, _currentVelocity.y, _currentVelocity.z);
            
            if (Mathf.Abs(_currentVelocity.y) >= _maximumSpeed)
                _currentVelocity = new Vector3(_currentVelocity.x, _maximumSpeed, _currentVelocity.z);
            
            if (Mathf.Abs(_currentVelocity.z) >= _maximumSpeed)
                _currentVelocity = new Vector3(_currentVelocity.x, _currentVelocity.z, _maximumSpeed);
            
            _rigidbody.velocity = _currentVelocity;
        }
        public Vector2 CurrentMoveDirection => _currentMoveDirection;
        public bool IsMoving => _isMoving;
        public float WalkSpeed => _walkSpeed;
        public float MaximumSpeed => _maximumSpeed;
        public void Subscribe()
        {
            _inputMoveHandler.InputChanged += Move;
        }

        public void Unsubscribe()
        {
            _inputMoveHandler.InputChanged -= Move;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
