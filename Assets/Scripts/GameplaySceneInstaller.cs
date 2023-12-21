using AssemblySystem.Manager;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AssemblyManager>().AsSingle();
    }
}
