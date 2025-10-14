using _Project.Scripts.Physics;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerView : PhysicsBody
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _bulletContainer;
        
        public Transform ShootPoint => _shootPoint;
        public Transform BulletContainer => _bulletContainer;
        public override PhysicsBodyType BodyType => PhysicsBodyType.Player;
    }
}