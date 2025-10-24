using _Project.Scripts.Services;

namespace _Project.Scripts.Enemy
{
    public class AsteroidParticleFactory : EntityAbstractFactory<AsteroidParticleView>
    {
        private const string ASTEROID_PARTICLE = "AsteroidParticle";
        private readonly AsteroidParticleView _asteroidParticlePrefab;
        public AsteroidParticleFactory(ResourceLoadingService resourceLoadingService)
        {
            _asteroidParticlePrefab = resourceLoadingService.Load<AsteroidParticleView>(ASTEROID_PARTICLE);
            Pool = new ObjectPool<AsteroidParticleView>(_asteroidParticlePrefab);
        }

        public override AsteroidParticleView Create()
        {
            var instance = Pool.Get();
            return instance;
        }

        public void Release(AsteroidParticleView particle)
        {
            Pool.Release(particle);
        }
    }
}