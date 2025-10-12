using System;
using _Project.Scripts.Health;
using _Project.Scripts.World;
using UnityEngine;

namespace _Project.Scripts.Shooting
{
    public class Bullet : MonoBehaviour
    {
        private WorldBoundsService _worldBoundsService;
        public event Action<Bullet> OnBulletDestroyRequested;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage();
                OnBulletDestroyRequested?.Invoke(this);
            }
        }

        public void Initialize(WorldBoundsService worldBoundsService)
        {
            _worldBoundsService = worldBoundsService;
        }

        private void Update()
        {
            if (_worldBoundsService.IsOutOfBounds(transform.position))
                OnBulletDestroyRequested?.Invoke(this);
        }
    }
}