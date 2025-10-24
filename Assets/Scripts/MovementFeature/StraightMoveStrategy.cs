using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public class StraightMoveStrategy : IMoveStrategy
    {
        private readonly float _speed ;
        private readonly Vector3 _direction;
        private readonly float _externalForceThreshold;
        public StraightMoveStrategy(float speed, Vector3 direction)
        {
            _speed = speed;
            _direction = direction;
        }
        public void Move(PhysicsBody body)
        {
            body.Velocity = _direction * _speed;
        }
    }
}