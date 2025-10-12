using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class AsteroidParticleFactory : EntityAbstractFactory<AsteroidParticleView>
    {
        private AsteroidParticleView _asteroidParticlePrefab;
        public AsteroidParticleFactory(DiContainer container, ResourceLoadingService resourceLoadingService) : base(container, resourceLoadingService)
        {
            _asteroidParticlePrefab = resourceLoadingService.Load<AsteroidParticleView>("AsteroidParticle");
            Pool = new ObjectPool<AsteroidParticleView>(_asteroidParticlePrefab);
        }

        public override AsteroidParticleView Create()
        {
            var instance = Pool.Get();
            instance.OnAsteroidParticleShot += Release;
            return instance;
        }

        private void Release(AsteroidParticleView particle)
        {
            Pool.Release(particle);
        }
    }
}