using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Shooting
{
    public class BulletSpawner
    {
        private const string BULLET = "Bullet";
        private readonly StrategyMoveAgent _moveAgent;
        private readonly WeaponConfig _weaponConfig;
        private readonly ObjectPool<Bullet> _bullets;

        public BulletSpawner(ResourceLoadingService resourceLoadingService, WeaponConfig weaponConfig,
            StrategyMoveAgent moveAgent)
        {
            _weaponConfig = weaponConfig;
            _moveAgent = moveAgent;
            var bulletPrefab = resourceLoadingService.Load<Bullet>(BULLET);
            _bullets = new ObjectPool<Bullet>(bulletPrefab);
        }


        public Bullet SpawnBullet(Transform point)
        {
            var bullet = _bullets.Get(point.position);
            var strategy = new StraightMoveStrategy(_weaponConfig.Speed, point.up);
            _moveAgent.AddMoveSubject(bullet, strategy);
            bullet.OnBulletDestroyRequested += RecycleBullet;
            return bullet;
        }


        private void RecycleBullet(Bullet bullet)
        {
            bullet.OnBulletDestroyRequested -= RecycleBullet;
            bullet.transform.rotation = Quaternion.identity;
            _moveAgent.RemoveMoveSubject(bullet);
            _bullets.Release(bullet);
        }
    }
}