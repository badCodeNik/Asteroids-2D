using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Physics
{
    public class CustomPhysicsService : ICustomPhysicsService, IFixedTickable
    {
        private readonly List<PhysicsBody> _bodies = new();
        private readonly List<PhysicsBody> _bodiesToAdd = new();
        private readonly List<PhysicsBody> _bodiesToRemove = new();
        private readonly ICollisionHandler _collisionHandler;

        public CustomPhysicsService(
            ICollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        public void RegisterBody(PhysicsBody body)
        {
            _bodiesToAdd.Add(body);
        }

        public void UnregisterBody(PhysicsBody body)
        {
            _bodiesToRemove.Add(body);
        }

        public void FixedTick()
        {
            ProcessPendingChanges();

            CheckCollisions();
        }


        private void CheckCollisions()
        {
            for (int i = 0; i < _bodies.Count; i++)
            {
                var bodyA = _bodies[i];
                if (!bodyA.IsActive || !bodyA.CanCollide) continue;

                for (int j = i + 1; j < _bodies.Count; j++)
                {
                    var bodyB = _bodies[j];
                    if (!bodyB.IsActive || !bodyB.CanCollide) continue;

                    if (!ShouldCollide(bodyA.BodyType, bodyB.BodyType))
                        continue;

                    if (CheckCircleCollision(bodyA, bodyB, out CollisionInfo info))
                    {
                        _collisionHandler.HandleCollision(bodyA, bodyB, info);
                    }
                }
            }
        }

        private bool ShouldCollide(PhysicsBodyType typeA, PhysicsBodyType typeB)
        {
            if (IsAsteroid(typeA) && IsAsteroid(typeB))
                return false;

            if ((IsAsteroid(typeA) && typeB == PhysicsBodyType.FlyingPlate) ||
                (IsAsteroid(typeB) && typeA == PhysicsBodyType.FlyingPlate))
                return false;

            return true;
        }

        private bool IsAsteroid(PhysicsBodyType type)
        {
            return type == PhysicsBodyType.Asteroid ||
                   type == PhysicsBodyType.AsteroidFragment;
        }

        private bool CheckCircleCollision(PhysicsBody a, PhysicsBody b, out CollisionInfo info)
        {
            info = default;

            Vector2 delta = b.Position - a.Position;
            float distanceSqr = delta.sqrMagnitude;
            float minDistance = a.Radius + b.Radius;
            float minDistanceSqr = minDistance * minDistance;

            if (distanceSqr < minDistanceSqr)
            {
                float distance = Mathf.Sqrt(distanceSqr);

                if (distance < 0.0001f)
                {
                    info.Normal = Vector2.up;
                    info.Penetration = minDistance;
                }
                else
                {
                    info.Normal = delta / distance;
                    info.Penetration = minDistance - distance;
                }

                info.ContactPoint = a.Position + info.Normal * a.Radius;
                return true;
            }

            return false;
        }

        private void ProcessPendingChanges()
        {
            foreach (var body in _bodiesToAdd)
            {
                if (!_bodies.Contains(body))
                    _bodies.Add(body);
            }

            _bodiesToAdd.Clear();

            foreach (var body in _bodiesToRemove)
            {
                _bodies.Remove(body);
            }

            _bodiesToRemove.Clear();
        }
    }

    public struct CollisionInfo
    {
        public Vector2 Normal;
        public float Penetration;
        public Vector2 ContactPoint;
    }
}