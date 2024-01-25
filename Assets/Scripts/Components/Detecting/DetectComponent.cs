using System;
using System.Collections;
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

        public Action<Transform> OnTargetSwitched;

        private Coroutine _detectionCoroutine;

        private RaycastHit[] _detectedRaycastHits;

        private bool _isInitialized = false;
        
        public void Init(DetectConfig detectConfig)
        {
            if (detectConfig == null)
                detectConfig = DetectConfig.DefaultConfig; 
            
            _targetComponent = detectConfig.TargetType;
            _detectionLayer = detectConfig.DetectionLayer;
            _detectionDelay = detectConfig.DetectionDelay;
            _detectionRadius = detectConfig.DetectionRadius;
            _detectionDistance = detectConfig.DetectionRadius;
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

        private void OnDisable()
        {
            StopAllCoroutines();
        }

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

            _currentTarget = null;
            for (var i = 0; i < _detectedRaycastHits.Length; ++i)
            {
                if(!_detectedRaycastHits[i].collider)
                    continue;
                
                if (!_detectedRaycastHits[i].collider.TryGetComponent(_targetComponent, out Component comp)) continue;

                if (!_currentTarget)
                {
                    _currentTarget = _detectedRaycastHits[i].transform;
                }
                else
                    _currentTarget = DistanceChecker.GetClosest(
                        transform.position, 
                        _currentTarget,
                        _detectedRaycastHits[i].transform);
                break;
                
            }
            OnTargetSwitched?.Invoke(_currentTarget);

            for (int i = 0; i < _detectedRaycastHits.Length; ++i)
            {
                _detectedRaycastHits[i] = default;
            }

        }

    }
}