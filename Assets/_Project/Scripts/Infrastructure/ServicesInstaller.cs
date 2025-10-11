using _Project.Scripts.Services;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ResourceLoadingService>().AsSingle();
    }
}
