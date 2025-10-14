using UnityEngine;

namespace _Project.Scripts.Physics
{
    public interface ICollisionHandler
    {
        void HandleCollision(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo info);
    }
}