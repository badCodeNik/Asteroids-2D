using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private KeyBindingsConfig _keyBindingsConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WorldConfig _worldConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private LaserConfig _laserConfig;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<KeyBindingsConfig>()
                .FromInstance(_keyBindingsConfig)
                .AsSingle();

            Container.Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();

            Container.Bind<WorldConfig>()
                .FromInstance(_worldConfig)
                .AsSingle();

            Container.Bind<EnemyConfig>()
                .FromInstance(_enemyConfig)
                .AsSingle();
            
            Container.Bind<BulletConfig>()
                .FromInstance(_bulletConfig)
                .AsSingle();
            
            Container.Bind<LaserConfig>()
                .FromInstance(_laserConfig)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<StrategyMoveAgent>().AsSingle();
        }
    }
}