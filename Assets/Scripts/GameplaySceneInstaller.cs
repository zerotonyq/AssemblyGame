using AssemblySystem.Manager;
using AssemblySystem.Scheme;
using UnityEngine;
using UserInputSystem.InputMoveSystem;
using UserInputSystem.SelectObjectSystem.KeyboardMouse.Model;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputActions>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<MouseMoveSelectHandler>().AsSingle().NonLazy();
        Container.Bind<InputMoveHandler>().AsSingle();
    }
}
