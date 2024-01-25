using System;
using CommonInterfaces;
using Game.Components.Rotating;
using Game.Entities.Enemy;
using MoveSystem;
using UnityEngine;
using UnityEngine.AI;
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
            _navMeshAgent.updatePosition = agentConfig.UpdatePosition;
            _navMeshAgent.updateRotation = agentConfig.UpdateRotation;
            _navMeshAgent.acceleration = agentConfig.Acceleration;
            _navMeshAgent.speed = _moveComponent.MaximumWalkSpeed;
            Subscribe();
           
            _isInitialized = true;
        }
        
        
        public void AssignTarget(Transform target)
        {
            if (!target)
                return;
            _currentTarget = target;
            _navMeshAgent.SetDestination(_currentTarget.position);
            _rotateComponent.LookAt(_currentTarget.position);
        }

        private void FixedUpdate()
        {
            _moveComponent.SetDirectionFromPosition(_navMeshAgent.nextPosition - transform.position);
        }

        public void Subscribe() =>_detectComponent.OnTargetSwitched += AssignTarget;
        public void Unsubscribe() => _detectComponent.OnTargetSwitched -= AssignTarget;
        
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