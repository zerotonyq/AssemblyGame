using System;
using CommonInterfaces;
using Game.Components.Rotating;
using Game.Entities.Enemy;
using MoveSystem;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Game.Components.Chasing
{
    public class ChaseComponent : MonoBehaviour, ISubscribable
    {
        private NavMeshAgent _navMeshAgent;
        private DetectComponent _detectComponent;
        private MoveComponent _moveComponent;
        private RotateComponent _rotateComponent;
        
        private Transform _currentTarget;

        private Vector3 _currentPathPosition;
        
        private bool _isInitialized;
        
        //For better AI. Chasing continues when target goes
        //for some time after going around the corner
        private float _additionFollowingTime;
        private float _additionFollowingDelta;
        private bool _isAdditionalFollowing;
        public void Init(
            MoveComponent moveComponent,
            RotateComponent rotateComponent,
            DetectComponent detectComponent, 
            NavMeshAgent agent, 
            NavMeshConfig agentConfig)
        {
            _currentPathPosition = transform.position;
            
            agentConfig ??= NavMeshConfig.defaultConfig;

            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
            _detectComponent = detectComponent;
            
            _navMeshAgent = agent;
            _navMeshAgent.stoppingDistance = agentConfig.StoppingDistance;
            _additionFollowingTime = agentConfig.AdditionFollowingTime;
            _additionFollowingDelta = agentConfig.AdditionFollowingDelta;
            _navMeshAgent.updatePosition = agentConfig.UpdatePosition;
            _navMeshAgent.updateRotation = agentConfig.UpdateRotation;
            _navMeshAgent.acceleration = agentConfig.Acceleration;
            _navMeshAgent.speed = _moveComponent.MaximumWalkSpeed;
            Subscribe();
           
            _isInitialized = true;
        }
        
        
        public void ChaseTargetPosition(Transform target)
        {
            if (!target)
            {
                if(!_currentTarget)
                    return;
                
                if (_isAdditionalFollowing)
                    return;
                
                MonoBehaviourExecuteTimer.StartExecuteTimer(
                    this,
                    _additionFollowingTime,
                    _additionFollowingDelta,
                    () => ChaseTargetPosition(_currentTarget),
                    () =>
                    {
                        _currentTarget = null;
                        _isAdditionalFollowing = false;
                    });
                
                _isAdditionalFollowing = true;
                return;
            }
            
            _currentTarget = target;
            
            _navMeshAgent.SetDestination(_currentTarget.position);
        }
        
        private void FixedUpdate()
        {
            _moveComponent.SetDirectionFromPosition(_navMeshAgent.nextPosition - transform.position);
        }
        public void Subscribe() =>_detectComponent.OnTargetDetected += ChaseTargetPosition;
        public void Unsubscribe() => _detectComponent.OnTargetDetected -= ChaseTargetPosition;
        
        public void OnEnable()
        {
            if (!_isInitialized)
                return;
            Subscribe();
        }

        public void OnDisable()
        {
            Unsubscribe();
        }
    }
}