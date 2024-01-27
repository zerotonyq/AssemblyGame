using System.Collections;
using Character;
using Game.Components;
using Game.Components.Chasing;
using Game.Components.Config;
using Game.Components.Jumping;
using Game.Components.Rotating;
using Game.Components.Rotating.Config;
using Game.Entities.Enemy.EventSubscriber;
using MoveSystem;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Zenject;

namespace Enemy
{
    public class EnemyFactory
    {
        private CoroutineExecuter _coroutineExecuter = new GameObject("coroutineExecuter").AddComponent<CoroutineExecuter>();
        
        [Inject] private GameObject _characterPrefab;
        
        public void CreateEnemy()
        {
            var currentEnemy = CharacterFactory.CreateCharacter(
                _characterPrefab, 
                "enemy",
                rotateConfig : new RotateConfig(autoRotateToMoveDirection: true));
            
            var detectComponent = currentEnemy.AddComponentToCharacter<DetectComponent>();
            detectComponent.Init(DetectConfig.DefaultConfig);
            
            var navMeshAgent = currentEnemy.AddComponentToCharacter<NavMeshAgent>();
            
            var chaseComponent = currentEnemy.AddComponentToCharacter<ChaseComponent>();
            chaseComponent.Init(
                currentEnemy.Components[typeof(MoveComponent)] as MoveComponent, 
                currentEnemy.Components[typeof(RotateComponent)] as RotateComponent, 
                detectComponent, 
                navMeshAgent, 
                null);
            
            var rotateComp = currentEnemy.Components[typeof(RotateComponent)] as RotateComponent;
            var jumpComp = currentEnemy.Components[typeof(JumpComponent)] as JumpComponent;
            
            EnemyEventSubscriber.SubscribeComponents(
                chaseComponent,
                detectComponent,
                rotateComp,
                jumpComp);
        }
        
        public void InstantiateEnemyPack(int count)
        {
            _coroutineExecuter.StartCoroutine(InstantiateEnemyPackCoroutine(count));
        }

        private IEnumerator InstantiateEnemyPackCoroutine(int count = 1, float delta = 0.1f)
        {
            for(int i = 0; i < count; i++)
            {
                CreateEnemy();
                
                yield return new WaitForSeconds(delta);
            }
        }
        

    }
}