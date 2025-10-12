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
        public void Move(GameObject moveSubject)
        {
            var direction = _target.position - moveSubject.transform.position;
            moveSubject.transform.position += direction.normalized * _speed * Time.deltaTime;
        }
    }
}