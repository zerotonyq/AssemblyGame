using System;
using System.Collections;
using System.Timers;
using Game.Components.Config;
using Unity.Jobs;
using UnityEngine;
using Utils;

namespace Game.Components
{
    public class DetectComponent : MonoBehaviour
    {
        private DetectConfig _detectConfig;  
        
        private float _detectionDelay;
        private float _detectionRadius;
        private int _detectionLayer;
        private float _detectionDistance;
        private Type _targetComponent;
        
        private Transform _currentTarget;

        public Action<Transform> OnTargetDetected;

        private Coroutine _detectionCoroutine;

        private RaycastHit[] _detectedRaycastHits;
        private RaycastHit[] _targetRayCastHitThroughWall = new RaycastHit[1];

        private bool _isInitialized = false;
        
        public void Init(DetectConfig detectConfig)
        {
            if (detectConfig == null)
                detectConfig = DetectConfig.DefaultConfig; 
            
            _targetComponent = detectConfig.TargetType;
            _detectionLayer = detectConfig.DetectionLayer;
            _detectionDelay = detectConfig.DetectionDelay;
            _detectionRadius = detectConfig.DetectionRadius;
            _detectionDistance = detectConfig.DetectionDistance;
            _detectedRaycastHits = new RaycastHit[detectConfig.DetectionObjectsCount];
            
            _detectionCoroutine = StartCoroutine(DetectionCoroutine());
            
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (!_isInitialized)
                return;
            
            StopCoroutine(_detectionCoroutine);
            
            _detectionCoroutine = StartCoroutine(DetectionCoroutine());
        }

        private void OnDisable() => StopAllCoroutines();
        
        private IEnumerator DetectionCoroutine()
        {
            while (true)
            {
                Detect();
                yield return new WaitForSeconds(_detectionDelay);    
            }
        }
        
        private void Detect()
        {
            Physics.SphereCastNonAlloc(
                transform.position, 
                _detectionRadius, 
                transform.forward,
                _detectedRaycastHits,
                _detectionDistance,
                1 << _detectionLayer);

            _currentTarget = TryGetTargetFromArray();
            
            if (!_currentTarget)
                return;
            
            Physics.Raycast(transform.position, _currentTarget.position - transform.position,  out RaycastHit targetRayCastHitThroughWall);

            if (!targetRayCastHitThroughWall.collider || 
                !targetRayCastHitThroughWall.collider.TryGetComponent(_targetComponent, out Component comp))
                _currentTarget = null;
            
            OnTargetDetected?.Invoke(_currentTarget);
            
            StructArrayCleaner.Clean(_detectedRaycastHits);
        }
        
        private Transform TryGetTargetFromArray()
        {
            Transform target = _currentTarget;
            
            for (var i = 0; i < _detectedRaycastHits.Length; ++i)
            {
                if(!_detectedRaycastHits[i].collider)
                    continue;
                
                if (!_detectedRaycastHits[i].collider.TryGetComponent(_targetComponent, out Component comp)) 
                    continue;

                if (!target)
                {
                    target = _detectedRaycastHits[i].transform;
                }
                else
                {
                    target = DistanceChecker.GetClosest(
                        transform.position, 
                        target,
                        _detectedRaycastHits[i].transform);
                }
                
                break;
            }
           
            return target;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + transform.forward * _detectionRadius, _detectionRadius);
        }
    }
}