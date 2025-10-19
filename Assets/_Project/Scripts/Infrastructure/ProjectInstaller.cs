using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private KeyBindingsConfig _keyBindingsConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WorldConfig _worldConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [FormerlySerializedAs("_bulletConfig")] [SerializeField] private WeaponConfig _weaponConfig;

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
            
            Container.Bind<WeaponConfig>()
                .FromInstance(_weaponConfig)
                .AsSingle();

            Container.Bind<ConfigLoader>().AsSingle();
            
            Container.BindInterfacesTo<ConfigProvider>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<StrategyMoveAgent>().AsSingle();
        }
    }
}