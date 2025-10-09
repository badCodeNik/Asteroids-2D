using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Input
{
    public class InputInstaller : MonoInstaller
    {
        [Inject] private ResourceLoadingService _resourceLoadingService;
        public override void InstallBindings()
        {
            var joystick = _resourceLoadingService.Load<Joystick>("UI/Joystick");
            Container.BindInstance(joystick).AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}