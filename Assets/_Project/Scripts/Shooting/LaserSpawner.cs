using System;
using _Project.Scripts.Configs;
using _Project.Scripts.Health;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Shooting
{
    public class LaserSpawner : ITickable
    {
        private readonly LaserConfig _config;
        private LaserView _laserPrefab;
        private ObjectPool<LaserView> _laserPool;
        public event Action<int> OnChargesChanged;
        private float _rechargeTimer;
        private RaycastHit2D[] _hits = new RaycastHit2D[20];

        private int _charges;

        public LaserSpawner(ResourceLoadingService resourceLoadingService, LaserConfig config)
        {
            _config = config;
            _laserPrefab = resourceLoadingService.Load<LaserView>("Laser");
            _laserPool = new ObjectPool<LaserView>(_laserPrefab);
            _charges = _config.MaxCharges;
            _rechargeTimer = _config.RechargeTime;
        }

        public async void ShootLaser(Vector2 position, Vector3 direction)
        {
            if (!CanShoot()) return;
            _charges--;
            OnChargesChanged?.Invoke(_charges);

            var hits = Physics2D.RaycastNonAlloc(
                position,
                direction,
                _hits,
                _config.MaxDistance
            );

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    var hit = _hits[i];
                    if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                        damageable.TakeDamage(true);
                }
            }

            var laserView = _laserPool.Get(position);
            laserView.Initialize(_config);
            laserView.Show(position, direction);
            await UniTask.Delay(TimeSpan.FromSeconds(_config.Duration));
            laserView.Hide();
            _laserPool.Release(laserView);
        }

        public void Tick()
        {
            if (_charges < _config.MaxCharges)
            {
                _rechargeTimer -= Time.deltaTime;
                if (_rechargeTimer <= 0)
                {
                    _charges++;
                    _rechargeTimer = _config.RechargeTime;
                }
            }
        }

        private bool CanShoot() => _charges > 0;
    }
}