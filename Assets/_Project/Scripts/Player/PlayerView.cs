using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _bulletContainer;
        public Transform ShootPoint => _shootPoint;
        public Transform BulletContainer => _bulletContainer;
    }
}