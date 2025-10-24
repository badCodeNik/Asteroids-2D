using _Project.Scripts.Input;
using _Project.Scripts.Physics;
using _Project.Scripts.Services;
using _Project.Scripts.World;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle().NonLazy();
            Container.Bind<TimerService>().AsTransient();
            Container.Bind<ResourceLoadingService>().AsSingle();
            Container.Bind<WorldBoundsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollisionHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomPhysicsService>().AsSingle();
            Container.Bind<RewardGivingService>().AsSingle().NonLazy();
        }
    }
}