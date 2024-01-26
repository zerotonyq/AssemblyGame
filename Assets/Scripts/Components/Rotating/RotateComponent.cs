using System;
using Game.Components.Rotating.Config;
using Game.Components.Rotating.Interfaces;
using UnityEngine;

namespace Game.Components.Rotating
{
    public class RotateComponent : MonoBehaviour, IRotatable
    {
        private float _rotationSpeed;
        private float _rotationEndDeltaDegrees;
        
        private bool _isRotating;

        private Vector3 _lookVector;

        private Quaternion _lookRotation;
        
        private bool _isInitialized = false;

        private Vector3 _lastFixedUpdatePosition;

        private bool _autoRotateToMoveDirection;
        public void Init(RotateConfig config)
        {
            _autoRotateToMoveDirection = config.AutoRotateToMoveDirection;
            _rotationSpeed = config.RotationMaxSpeed;
            _rotationEndDeltaDegrees = config.RotationEndDeltaDegrees;
        }

        public void LookAt(Transform other)
        {
            if(!other)
                return;
            _lookVector = other.position - transform.position;
            _lookRotation = Quaternion.LookRotation(_lookVector, Vector3.up);
            _isRotating = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay( new Ray(transform.position, transform.forward));
        }

        private void FixedUpdate()
        {
            if (_autoRotateToMoveDirection)
            {
                var direction = (transform.position - _lastFixedUpdatePosition).normalized;
                if (direction.magnitude <= 0.01f)
                    return;
                var forwardRotation = Quaternion.LookRotation(
                    (transform.position - _lastFixedUpdatePosition), 
                    Vector3.up);
                ApplyPlaneRotation(forwardRotation);
            }
            
            _lastFixedUpdatePosition = transform.position;
            
            if (!_isRotating)
                return;

            if (Quaternion.Angle(transform.rotation, _lookRotation) <= _rotationEndDeltaDegrees)
            {
                _isRotating = false;
            }
            
            ApplyPlaneRotation(_lookRotation);
        }

        private void ApplyPlaneRotation(Quaternion rotation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                new Quaternion(0,rotation.y, 0, rotation.w), 
                _rotationSpeed);
        }

        public float RotationEndDeltaDegrees => _rotationEndDeltaDegrees;
        public bool IsRotating => _isRotating;
        public float RotationSpeed => _rotationSpeed;

    }
}