using System;
using _Project.Scripts.Health;
using _Project.Scripts.Physics;

namespace _Project.Scripts.Enemy
{
    public class AsteroidParticleView : PhysicsBody, IDamageable
    {
        public event Action<AsteroidParticleView> OnAsteroidParticleShot;

        public void TakeDamage(bool destroyImmediately = false)
        {
            OnAsteroidParticleShot?.Invoke(this);
        }

        public override PhysicsBodyType BodyType => PhysicsBodyType.AsteroidFragment;
    }
}