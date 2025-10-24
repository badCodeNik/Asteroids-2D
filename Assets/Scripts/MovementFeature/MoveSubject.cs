using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public class MoveSubject
    {
        public PhysicsBody Body;
        public IMoveStrategy Strategy;
        public Transform Target;
            
        public MoveSubject(PhysicsBody body, IMoveStrategy strategy, Transform target = null)
        {
            Body = body;
            Strategy = strategy;
            Target = target;
        }
    }
}