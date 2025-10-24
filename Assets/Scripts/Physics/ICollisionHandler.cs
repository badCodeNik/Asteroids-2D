namespace _Project.Scripts.Physics
{
    public interface ICollisionHandler
    {
        void HandleCollision(PhysicsBody firstBody, PhysicsBody secondBody, CollisionInfo info);
    }
}