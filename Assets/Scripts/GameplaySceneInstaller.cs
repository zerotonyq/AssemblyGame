using Cinemachine;
using Game;
using Enemy;
using Game.UserInputSystem.InputMoveSystem;
using UserInputSystem.SelectObjectSystem.KeyboardMouse.Handler;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera virtualCameraPrefab;
    [SerializeField] private GameObject defaultCharacterPrefab;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputActions>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<MouseMoveSelectHandler>().AsSingle().NonLazy();
        Container.Bind<InputMoveHandler>().AsSingle();
        Container.Bind<InputJumpHandler>().AsSingle();
        Container.Bind<EnemyFactory>().AsSingle().WithArguments(defaultCharacterPrefab); // delete with args
        Container.Bind<StartupGameplayScene>().AsSingle().WithArguments(defaultCharacterPrefab, virtualCameraPrefab).NonLazy();
        

    }
}
