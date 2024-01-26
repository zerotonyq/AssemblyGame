using Character;
using Cinemachine;
using Enemy;
using Game.Components;
using Game.Components.Jumping;
using Game.Components.Jumping.Interfaces;
using Game.UserInputSystem.InputMoveSystem;
using MoveSystem;
using UnityEngine;
using Zenject;

namespace Game
{
    public class StartupGameplayScene
    {
        private GameObject _defaultCharacterPrefab;
        private Character.Character _mainCharacter;
        private InputMoveHandler _currentMoveHandler;
        private InputJumpHandler _currentJumpHandler;
        private CinemachineVirtualCamera _virtualCameraPrefab;
        private EnemyFactory _enemyFactory;
        
        [Inject]
        public StartupGameplayScene(GameObject defaultCharacterPrefab, 
            InputMoveHandler moveHandler,
            InputJumpHandler inputJumpHandler,
            CinemachineVirtualCamera virtualCameraPrefab,
            EnemyFactory enemyFactory)
        {
            _virtualCameraPrefab = virtualCameraPrefab;
            _currentMoveHandler = moveHandler;
            _currentJumpHandler = inputJumpHandler;
            _defaultCharacterPrefab = defaultCharacterPrefab;
            _enemyFactory = enemyFactory;
            Init();
        }

        private void Init()
        {
            _enemyFactory.InstantiateEnemyPack(1);
            
            _mainCharacter = 
                CharacterFactory.CreateCharacter(_defaultCharacterPrefab, "player");
            
            _mainCharacter.AddComponentToCharacter<PlayerComponent>();
            
            var mainCharacterMoveComp = _mainCharacter.Components[typeof(MoveComponent)];
            _currentMoveHandler.ChangeMovable(mainCharacterMoveComp as IMovable);

            var mainCharacterJumpComponent = _mainCharacter.Components[typeof(JumpComponent)];
            _currentJumpHandler.ChangeJumpable(mainCharacterJumpComponent as IJumpable);
            
            var virtualCamera = CinemachineBrain.Instantiate(_virtualCameraPrefab);
            virtualCamera.Follow = mainCharacterMoveComp.transform;
        }
    }
}