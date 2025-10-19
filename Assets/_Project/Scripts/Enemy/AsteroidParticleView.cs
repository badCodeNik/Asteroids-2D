using System;
using _Project.Scripts.Health;
using _Project.Scripts.Physics;
using _Project.Scripts.Services;

namespace _Project.Scripts.Enemy
{
    public class AsteroidParticleView : PhysicsBody, IDamageable, IEnemyType
    {
        public event Action<AsteroidParticleView> OnAsteroidParticleShot;

        public void TakeDamage(bool destroyImmediately = false)
        {
            OnAsteroidParticleShot?.Invoke(this);
        }

        public override PhysicsBodyType BodyType => PhysicsBodyType.AsteroidFragment;
        public EnemyType Type => EnemyType.AsteroidParticle;

    }
}