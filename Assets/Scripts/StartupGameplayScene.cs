using Character;
using Cinemachine;
using Enemy;
using Game.Components;
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
        private InputMoveHandler _currentInputMoveHandler;
        private CinemachineVirtualCamera _virtualCameraPrefab;
        private EnemyFactory _enemyFactory;
        [Inject]
        public StartupGameplayScene(GameObject defaultCharacterPrefab, 
            InputMoveHandler inputInputMoveHandler,
            CinemachineVirtualCamera virtualCameraPrefab,
            EnemyFactory enemyFactory)
        {
            _virtualCameraPrefab = virtualCameraPrefab;
            _currentInputMoveHandler = inputInputMoveHandler;
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
            _currentInputMoveHandler.ChangeMovable(mainCharacterMoveComp as IMovable);
            var virtualCamera = CinemachineBrain.Instantiate(_virtualCameraPrefab);
            virtualCamera.Follow = mainCharacterMoveComp.transform;
        }
    }
}