using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidParticleFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FlyingPlateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }
    }
}