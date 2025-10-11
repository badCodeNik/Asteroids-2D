using _Project.Scripts.MovementFeature;
using _Project.Scripts.Services;
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
        }
    }
}