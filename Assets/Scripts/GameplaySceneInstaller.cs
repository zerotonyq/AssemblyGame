using AssemblySystem.Manager;
using UnityEngine;
using UserInputSystem.SelectObjectSystem.KeyboardMouse.Model;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    
    
    public override void InstallBindings()
    {
        Container.Bind<AssemblyManager>().AsSingle().NonLazy();
        Container.Bind<PlayerInputActions>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<MouseSelectHandler>().AsSingle().NonLazy();
    }
}
