using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint => _shootPoint;
    }
}