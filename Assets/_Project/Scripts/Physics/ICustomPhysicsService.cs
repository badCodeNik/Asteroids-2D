namespace _Project.Scripts.Physics
{
    public interface ICustomPhysicsService
    {
        void RegisterBody(PhysicsBody body);
        void UnregisterBody(PhysicsBody body);
    }}