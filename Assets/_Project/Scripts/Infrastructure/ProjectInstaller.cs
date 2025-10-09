using System.Collections.Generic;
using _Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private KeyBindingsConfig _keyBindingsConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<KeyBindingsConfig>()
                .FromInstance(_keyBindingsConfig)
                .AsSingle();
            Container.Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();
        }
    }
}