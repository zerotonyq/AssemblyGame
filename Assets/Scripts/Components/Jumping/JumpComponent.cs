using System;
using Game.Components.Jumping.Config;
using Game.Components.Jumping.Interfaces;
using UnityEngine;

namespace Game.Components.Jumping
{    
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class JumpComponent : MonoBehaviour, IJumpable
    {
        private Rigidbody _rigidbody;
        private Collider _collider;
        
        private float _jumpForce;
        private int _floorLayerMask;
        
        private Vector3 _checkSurfaceLocalPoint;
        private Vector3 _checkSurfaceBoxHalf;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _checkSurfaceLocalPoint =  new Vector3(0, _collider.bounds.size.y/2, 0);
            _checkSurfaceBoxHalf = new Vector3(0.5f, 0.3f, 0.5f);
        }

        

        public void Init(JumpConfig settings)
        {
            _jumpForce = settings.JumpForce;
            _floorLayerMask = settings.FloorLayerMask;
        }

        public void Jump()
        {
            if (!CheckSurfaceUnderCharacter())
                return;
            _rigidbody.velocity = new Vector3(
                _rigidbody.velocity.x,
                _jumpForce,
                _rigidbody.velocity.z);
            
        }

        private bool CheckSurfaceUnderCharacter()
        {
            Collider[] results = new Collider[3];
            
            var size = Physics.OverlapBoxNonAlloc(
                transform.position - _checkSurfaceLocalPoint, 
                _checkSurfaceBoxHalf, 
                results, 
                Quaternion.identity,
                1 << _floorLayerMask);
            
            for (int i = 0; i < results.Length; ++i)
            {
                if (results[i])
                    return true;
            }

            return false;
        }

        public float JumpForce => _jumpForce;
    }
}