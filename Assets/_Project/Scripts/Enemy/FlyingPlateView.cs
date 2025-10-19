using System;
using _Project.Scripts.Health;
using _Project.Scripts.Physics;

namespace _Project.Scripts.Enemy
{
    public class FlyingPlateView : PhysicsBody, IDamageable 
    {
        public event Action<FlyingPlateView> OnFlyingPlateShot;
        public void TakeDamage(bool destroyImmediately = false)
        {
            OnFlyingPlateShot?.Invoke(this);
        }

        public override PhysicsBodyType BodyType => PhysicsBodyType.FlyingPlate;
    }
}