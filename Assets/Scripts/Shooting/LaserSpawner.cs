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
        private const string LASER = "Laser";
        private RaycastHit2D[] _hits = new RaycastHit2D[20];
        private readonly ObjectPool<LaserView> _laserPool;
        private readonly WeaponConfig _config;
        private float _rechargeTimer;
        private int _charges;


        private bool CanShoot() => _charges > 0;
        public event Action<int> OnChargesChanged;
        public event Action<float> OnRechargeTimerChanged;

        public LaserSpawner(ResourceLoadingService resourceLoadingService, WeaponConfig config)
        {
            _config = config;
            var laserPrefab = resourceLoadingService.Load<LaserView>(LASER);
            _laserPool = new ObjectPool<LaserView>(laserPrefab);
            _charges = _config.MaxCharges;
            _rechargeTimer = _config.RechargeTime;
        }

        public async void ShootLaser(Vector2 position, Vector3 direction)
        {
            if (!CanShoot()) return;
            _charges--;
            OnChargesChanged?.Invoke(_charges);

            Vector2 boxSize = new Vector2(_config.LaserWidth, _config.LaserWidth);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var hits = Physics2D.BoxCastNonAlloc(
                position,
                boxSize,
                angle,
                direction.normalized,
                _hits,
                _config.LaserMaxDistance
            );

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    var hit = _hits[i];
                    if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable.TakeDamage(true);
                    }
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
                OnRechargeTimerChanged?.Invoke(_rechargeTimer);
                if (!(_rechargeTimer <= 0)) return;
                
                _charges++;
                OnChargesChanged?.Invoke(_charges);
                _rechargeTimer = _config.RechargeTime;
            }
        }
    }
}