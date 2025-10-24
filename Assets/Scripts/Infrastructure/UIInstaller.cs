using _Project.Scripts.UI;
using _Project.Scripts.UI.Models;
using _Project.Scripts.UI.ViewModels;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private CanvasRoot _canvasRoot;
        public override void InstallBindings()
        {
            Container.BindInstance(_canvasRoot).AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
            Container.Bind<PlayerHUDViewModel>().AsSingle();;
        }
    }
}