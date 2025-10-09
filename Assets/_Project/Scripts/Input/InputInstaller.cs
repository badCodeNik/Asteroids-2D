using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Input
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var resourceLoadingService = Container.Resolve<ResourceLoadingService>();
            var joystick = resourceLoadingService.Load<Joystick>("UI/Joystick");
            Container.BindInstance(joystick).AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}