using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public class FollowPlayerMoveStrategy : IMoveStrategy
    {
        private readonly Transform _target;
        private readonly float _speed;


        public FollowPlayerMoveStrategy(Transform target, float speed)
        {
            _target = target;
            _speed = speed;
        }

        public void Move(PhysicsBody body)
        {
            if (_target == null) return;

            Vector2 direction = ((Vector2)_target.position - body.Position).normalized;
            body.Velocity = direction * _speed ;
        }
    }
}