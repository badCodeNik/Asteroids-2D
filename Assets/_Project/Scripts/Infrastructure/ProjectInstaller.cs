using System.Collections.Generic;
using _Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private Config[] _configs;

        public override void InstallBindings()
        {
            Container.Bind<IEnumerable<Config>>()
                .FromInstance(_configs)
                .AsSingle();
        }
    }
}