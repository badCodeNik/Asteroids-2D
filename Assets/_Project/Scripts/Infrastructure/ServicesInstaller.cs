using _Project.Scripts.Services;
using _Project.Scripts.World;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TimerService>().AsTransient();
            Container.Bind<ResourceLoadingService>().AsSingle();
            Container.Bind<WorldBoundsService>().AsSingle();
        }
    }
}