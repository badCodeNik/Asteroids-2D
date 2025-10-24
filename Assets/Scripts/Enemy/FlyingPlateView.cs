using System;
using _Project.Scripts.Health;
using _Project.Scripts.Physics;
using _Project.Scripts.Services;

namespace _Project.Scripts.Enemy
{
    public class FlyingPlateView : PhysicsBody, IDamageable , IEnemyType
    {
        public event Action<FlyingPlateView> OnFlyingPlateShot;
        public void TakeDamage(bool destroyImmediately = false)
        {
            OnFlyingPlateShot?.Invoke(this);
        }

        public override PhysicsBodyType BodyType => PhysicsBodyType.FlyingPlate;
        public EnemyType Type => EnemyType.FlyingPlate;
    }
}