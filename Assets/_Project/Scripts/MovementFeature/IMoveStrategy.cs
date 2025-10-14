using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public interface IMoveStrategy
    {
        void Move(PhysicsBody body);
    }
}