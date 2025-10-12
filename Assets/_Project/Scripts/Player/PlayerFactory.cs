using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerFactory : EntityAbstractFactory<PlayerView>, IInitializable
    {
        private readonly DiContainer _container;
        private readonly ResourceLoadingService _resourceLoadingService;
        private readonly SignalBus _signalBus;
        private readonly PlayerView _playerView;

        public PlayerFactory(DiContainer container, ResourceLoadingService resourceLoadingService,
            SignalBus signalBus) : base(
            container, resourceLoadingService)
        {
            _container = container;
            _resourceLoadingService = resourceLoadingService;
            _signalBus = signalBus;
        }

        public override PlayerView Create()
        {
            var playerPrefab = _resourceLoadingService.Load<PlayerView>("Player");
            var playerView = _container.InstantiatePrefabForComponent<PlayerView>(playerPrefab);

            _container.Bind<PlayerView>().FromInstance(playerView).AsSingle();
            _signalBus.Fire(new Signals.PlayerSpawnedSignal(playerView.transform));
            return playerView;
        }

        public void Initialize()
        {
            Create();
        }
    }
}