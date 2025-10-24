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
                var firstBody = _bodies[i];
                if (!firstBody.IsActive || !firstBody.CanCollide) continue;

                for (int j = i + 1; j < _bodies.Count; j++)
                {
                    var secondBody = _bodies[j];
                    if (!secondBody.IsActive || !secondBody.CanCollide) continue;

                    if (!ShouldCollide(firstBody.BodyType, secondBody.BodyType))
                        continue;

                    if (CheckCircleCollision(firstBody, secondBody, out CollisionInfo info))
                    {
                        _collisionHandler.HandleCollision(firstBody, secondBody, info);
                    }
                }
            }
        }

        private bool ShouldCollide(PhysicsBodyType firstBody, PhysicsBodyType typeB)
        {
            if (IsAsteroid(firstBody) && IsAsteroid(typeB))
                return false;

            if ((IsAsteroid(firstBody) && typeB == PhysicsBodyType.FlyingPlate) ||
                (IsAsteroid(typeB) && firstBody == PhysicsBodyType.FlyingPlate))
                return false;

            return true;
        }

        private bool IsAsteroid(PhysicsBodyType type)
        {
            return type == PhysicsBodyType.Asteroid ||
                   type == PhysicsBodyType.AsteroidFragment;
        }

        private bool CheckCircleCollision(PhysicsBody firstBody, PhysicsBody secondBody, out CollisionInfo info)
        {
            info = default;

            Vector2 delta = secondBody.Position - firstBody.Position;
            float distanceSqr = delta.sqrMagnitude;
            float minDistance = firstBody.Radius + secondBody.Radius;
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
    }
}