using _Project.Scripts.MovementFeature;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private ResourceLoadingService _resourceLoadingService;

        public override void InstallBindings()
        {
            var player = _resourceLoadingService.Load<PlayerView>("Player");
            var playerView = Container.InstantiatePrefabForComponent<PlayerView>(player);
            Container.BindInstance(playerView).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
        }
    }
}