using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public class StraightMoveStrategy : IMoveStrategy
    {
        private readonly float _speed ;
        private readonly Vector3 _direction;

        public StraightMoveStrategy(float speed, Vector3 direction)
        {
            _speed = speed;
            _direction = direction;
        }
        public void Move(GameObject moveSubject)
        {
            moveSubject.transform.Translate(_direction * _speed  * Time.deltaTime);
        }
    }
}