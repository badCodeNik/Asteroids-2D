using _Project.Scripts.Physics;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerFactory : EntityAbstractFactory<PlayerView>, IInitializable
    {
        private readonly DiContainer _container;
        private readonly ResourceLoadingService _resourceLoadingService;
        private readonly SignalBus _signalBus;
        private readonly ICustomPhysicsService _customPhysics;
        private readonly PlayerView _playerView;

        public PlayerFactory(DiContainer container, ResourceLoadingService resourceLoadingService,
            SignalBus signalBus, ICustomPhysicsService customPhysics) : base(
            container, resourceLoadingService)
        {
            _container = container;
            _resourceLoadingService = resourceLoadingService;
            _signalBus = signalBus;
            _customPhysics = customPhysics;
        }

        public override PlayerView Create()
        {
            var playerPrefab = _resourceLoadingService.Load<PlayerView>("Player");
            var playerView = _container.InstantiatePrefabForComponent<PlayerView>(playerPrefab);

            _container.Bind<PlayerView>().FromInstance(playerView).AsSingle();
            _signalBus.Fire(new Signals.PlayerSpawnedSignal(playerView.transform));
            _customPhysics.RegisterBody(playerView);
            return playerView;
        }

        public void Initialize()
        {
            Create();
        }
    }
}