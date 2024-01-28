using Character;
using Cinemachine;
using Game;
using Enemy;
using Game.Components.Inventory.Data;
using Game.UserInputSystem.InputMoveSystem;
using UserInputSystem.SelectObjectSystem.KeyboardMouse.Handler;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera virtualCameraPrefab;
    [SerializeField] private GameObject defaultCharacterPrefab;
    [FormerlySerializedAs("defaultInventoryConfig")] [SerializeField] private InventoryInitData defaultInventoryInitData;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputActions>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<MouseMoveSelectHandler>().AsSingle().NonLazy();
        Container.Bind<InputMoveHandler>().AsSingle();
        Container.Bind<InputJumpHandler>().AsSingle();
        Container.Bind<CharacterFactory>().AsSingle().WithArguments(defaultCharacterPrefab, defaultInventoryInitData);
        Container.Bind<EnemyFactory>().AsSingle();
        Container.Bind<StartupGameplayScene>().AsSingle().WithArguments(virtualCameraPrefab).NonLazy();
        

    }
}
