using _Project.Scripts.Player;
using Zenject;

namespace _Project.Scripts.Services
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly ResourceLoadingService _resourceLoadingService;
        private readonly SignalBus _signalBus;
        private readonly PlayerView _playerView;

        public PlayerFactory(DiContainer container, ResourceLoadingService resourceLoadingService, SignalBus signalBus)
        {
            _container = container;
            _resourceLoadingService = resourceLoadingService;
            _signalBus = signalBus;
        }

        public void SpawnPlayer()
        {
            var playerPrefab = _resourceLoadingService.Load<PlayerView>("Player");
            var playerView = _container.InstantiatePrefabForComponent<PlayerView>(playerPrefab);

            _container.Bind<PlayerView>().FromInstance(playerView).AsSingle();
            _signalBus.Fire(new Signals.PlayerSpawnedSignal(playerView));
        }
    }
}