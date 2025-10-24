using System;
using _Project.Scripts.Physics;

[Serializable]
public class PhysicsSettings
{
    public float BounceForce = 15f;
}

public class CollisionHandler : ICollisionHandler
{
    private readonly float _bounceForce = new PhysicsSettings().BounceForce;

    public void HandleCollision(PhysicsBody firstBody, PhysicsBody secondBody, CollisionInfo info)
    {
        if (IsPlayerVsEnemy(firstBody, secondBody, out var player, out var enemy))
        {
            HandlePlayerEnemyCollision(player, enemy, info);
            return;
        }
    }

    private bool IsPlayerVsEnemy(PhysicsBody firstBody, PhysicsBody secondBody,
        out PhysicsBody player, out PhysicsBody enemy)
    {
        player = null;
        enemy = null;

        if (firstBody.BodyType == PhysicsBodyType.Player && IsEnemy(secondBody.BodyType))
        {
            player = firstBody;
            enemy = secondBody;
            return true;
        }

        if (secondBody.BodyType == PhysicsBodyType.Player && IsEnemy(firstBody.BodyType))
        {
            player = secondBody;
            enemy = firstBody;
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

    private void SeparateBodies(PhysicsBody firstBody, PhysicsBody secondBody, CollisionInfo info)
    {
        float halfPenetration = info.Penetration * 0.5f;

        firstBody.Position -= info.Normal * halfPenetration;
        secondBody.Position += info.Normal * halfPenetration;
    }

    private void ApplyBounce(PhysicsBody firstBody, PhysicsBody secondBody, CollisionInfo info)
    {
        firstBody.ApplyForce(-info.Normal * _bounceForce);
        secondBody.ApplyForce(info.Normal * _bounceForce);
    }
}