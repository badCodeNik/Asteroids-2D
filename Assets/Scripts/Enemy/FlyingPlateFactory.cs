using System.Collections.Generic;
using _Project.Scripts.Enemy;
using _Project.Scripts.World;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class FlyingPlateFactory : EntityAbstractFactory<FlyingPlateView>
    {
        private const string FLYING_PLATE = "FlyingPlate";
        private readonly WorldBoundsService _worldBounds;
        private Queue<FlyingPlateView> _pool;

        public FlyingPlateFactory(ResourceLoadingService resourceLoadingService,
            WorldBoundsService worldBounds) 
        {
            _worldBounds = worldBounds;
            var flyingPlatePrefab = resourceLoadingService.Load<FlyingPlateView>(FLYING_PLATE);
            Pool = new ObjectPool<FlyingPlateView>(flyingPlatePrefab);
        }

        public override FlyingPlateView Create()
        {
            Vector2 spawnPos = _worldBounds.GetRandomOutOfWorldPosition();
            var instance = Pool.Get(spawnPos);
            return instance;
        }

        public void Release(FlyingPlateView plate)
        {
            Pool.Release(plate);
        }
    }
}