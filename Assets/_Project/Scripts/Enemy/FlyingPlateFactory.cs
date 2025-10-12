using System.Collections.Generic;
using _Project.Scripts.Enemy;
using _Project.Scripts.World;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class FlyingPlateFactory : EntityAbstractFactory<FlyingPlateView>
    {
        private readonly WorldBoundsService _worldBounds;
        private Queue<FlyingPlateView> _pool;
        private FlyingPlateView _flyingPlatePrefab;

        public FlyingPlateFactory(DiContainer container, ResourceLoadingService resourceLoadingService,
            WorldBoundsService worldBounds) : base(container, resourceLoadingService)
        {
            _worldBounds = worldBounds;
            _flyingPlatePrefab = resourceLoadingService.Load<FlyingPlateView>("FlyingPlate");
            Pool = new ObjectPool<FlyingPlateView>(_flyingPlatePrefab);
        }

        public override FlyingPlateView Create()
        {
            Vector2 spawnPos = _worldBounds.GetRandomOutOfWorldPosition();
            var instance = Pool.Get(spawnPos);
            instance.OnFlyingPlateShot += Release;
            return instance;
        }

        private void Release(FlyingPlateView plate)
        {
            Pool.Release(plate);
        }
    }
}