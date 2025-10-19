using System;
using _Project.Scripts.Health;
using _Project.Scripts.Physics;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class AsteroidView : PhysicsBody, IDamageable, IEnemyType
    {
        public event Action<AsteroidView> OnAsteroidDestroyed;
        public event Action<AsteroidView> OnAsteroidShot;
        public Vector2 Direction { get; private set; }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void TakeDamage(bool destroyImmediately = false)
        {
            if (destroyImmediately) OnAsteroidDestroyed?.Invoke(this);
            else OnAsteroidShot?.Invoke(this);
        }

        public override PhysicsBodyType BodyType => PhysicsBodyType.Asteroid;
        public EnemyType Type => EnemyType.Asteroid;
    }
}