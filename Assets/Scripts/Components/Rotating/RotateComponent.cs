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
        public void Init(RotateConfig config)
        {
            _rotationSpeed = config.RotationMaxSpeed;
            _rotationEndDeltaDegrees = config.RotationEndDeltaDegrees;
        }

        public void LookAt(Vector3 positon)
        {
            _lookVector = positon - transform.position;
            _lookRotation = Quaternion.LookRotation(_lookVector, Vector3.up);
            _isRotating = true;
        }
        
        private void Update()
        {
            if (!_isRotating)
                return;
            
            if (Quaternion.Angle(transform.rotation, _lookRotation) <= _rotationEndDeltaDegrees)
                _isRotating = false;
            
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                new Quaternion(0,_lookRotation.y, 0, _lookRotation.w), 
                _rotationSpeed * Time.deltaTime);
        }
        
        public float RotationEndDeltaDegrees => _rotationEndDeltaDegrees;
        public bool IsRotating => _isRotating;
        public float RotationSpeed => _rotationSpeed;
    }
}