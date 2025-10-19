using System;
using _Project.Scripts.Physics;

[Serializable]
public class PhysicsSettings
{
    public float BounceForce = 15f;
}

public class CollisionHandler : ICollisionHandler
{
    private readonly float _bounceForce;

    public CollisionHandler()
    {
        _bounceForce = new PhysicsSettings().BounceForce;
    }

    public void HandleCollision(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo info)
    {
        if (IsPlayerVsEnemy(bodyA, bodyB, out var player, out var enemy))
        {
            HandlePlayerEnemyCollision(player, enemy, info);
            return;
        }
    }

    private bool IsPlayerVsEnemy(PhysicsBody a, PhysicsBody b,
        out PhysicsBody player, out PhysicsBody enemy)
    {
        player = null;
        enemy = null;

        if (a.BodyType == PhysicsBodyType.Player && IsEnemy(b.BodyType))
        {
            player = a;
            enemy = b;
            return true;
        }

        if (b.BodyType == PhysicsBodyType.Player && IsEnemy(a.BodyType))
        {
            player = b;
            enemy = a;
            return true;
        }

        return false;
    }


    private bool IsEnemy(PhysicsBodyType type)
    {
        return type == PhysicsBodyType.Asteroid ||
               type == PhysicsBodyType.AsteroidFragment ||
               type == PhysicsBodyType.FlyingPlate;
    }

    private void HandlePlayerEnemyCollision(PhysicsBody player, PhysicsBody enemy, CollisionInfo info)
    {
        SeparateBodies(player, enemy, info);

        ApplyBounce(player, enemy, info);
    }

    private void SeparateBodies(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo info)
    {
        float halfPenetration = info.Penetration * 0.5f;

        bodyA.Position -= info.Normal * halfPenetration;
        bodyB.Position += info.Normal * halfPenetration;
    }

    private void ApplyBounce(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo info)
    {
        bodyA.ApplyForce(-info.Normal * _bounceForce);
        bodyB.ApplyForce(info.Normal * _bounceForce);
    }
}