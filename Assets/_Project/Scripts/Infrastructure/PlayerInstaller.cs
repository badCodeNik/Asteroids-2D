using _Project.Scripts.MovementFeature;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Shooting;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private ResourceLoadingService _resourceLoadingService;

        public override void InstallBindings()
        {
            Container.DeclareSignal<Signals.PlayerSpawnedSignal>();
            
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShootFeature>().AsSingle().NonLazy();
        }
    }
}