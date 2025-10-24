using System;
using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerView : PhysicsBody
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private ParticleSystem _particleSystem;
        public event Action OnDamageTaken;

        public Transform ShootPoint => _shootPoint;
        public Transform BulletContainer => _bulletContainer;
        public override PhysicsBodyType BodyType => PhysicsBodyType.Player;

        public override void ApplyForce(Vector2 force)
        {
            base.ApplyForce(force);
            OnDamageTaken?.Invoke();
        }

        public void EnableParticles(bool enable)
        {
            if(enable) _particleSystem.Play();
            else _particleSystem.Stop();
        }
    }
}