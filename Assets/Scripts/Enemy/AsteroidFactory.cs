using _Project.Scripts.Enemy;
using _Project.Scripts.World;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class AsteroidFactory : EntityAbstractFactory<AsteroidView>
    {
        private readonly WorldBoundsService _worldBounds;
        private AsteroidView _asteroidPrefab;

        public AsteroidFactory(ResourceLoadingService resourceLoadingService,
            WorldBoundsService worldBounds)
        {
            _worldBounds = worldBounds;
            _asteroidPrefab = resourceLoadingService.Load<AsteroidView>("Asteroid");
            Pool = new ObjectPool<AsteroidView>(_asteroidPrefab);
        }

        public override AsteroidView Create()
        {
            var instance = Pool.Get(_worldBounds.GetRandomOutOfWorldPosition());
            instance.SetDirection(_worldBounds.GetRandomInWorldPosition() - (Vector2)instance.transform.position);
            return instance;
        }

        public void Release(AsteroidView asteroid)
        {
            Pool.Release(asteroid);
        }
    }
}