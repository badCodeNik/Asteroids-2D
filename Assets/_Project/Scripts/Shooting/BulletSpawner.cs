using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Shooting
{
    public class BulletSpawner
    {
        private Transform _container;
        private readonly StrategyMoveAgent _moveAgent;
        private readonly WeaponConfig _weaponConfig;
        private readonly Bullet _bulletPrefab;
        private readonly ObjectPool<Bullet> _bullets;
        
        public BulletSpawner(ResourceLoadingService resourceLoadingService, WeaponConfig weaponConfig, StrategyMoveAgent moveAgent, SignalBus signalBus)
        {
            _weaponConfig = weaponConfig;
            _moveAgent = moveAgent;
            _bulletPrefab = resourceLoadingService.Load<Bullet>("Bullet");
            _bullets = new ObjectPool<Bullet>(_bulletPrefab);
            signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetPlayer);
        }

        private void SetPlayer(Signals.PlayerSpawnedSignal signal)
        {
            _container = signal.PlayerView.GetComponent<PlayerView>().BulletContainer;
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