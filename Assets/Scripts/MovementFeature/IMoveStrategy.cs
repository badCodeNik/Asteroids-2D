using _Project.Scripts.Physics;

namespace _Project.Scripts.MovementFeature
{
    public interface IMoveStrategy
    {
        void Move(PhysicsBody body);
    }
}