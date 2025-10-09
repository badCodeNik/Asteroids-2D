using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : Config
    {
        public float Acceleration = 5;
        public float RotationSpeed;
        public float Drag = 0.98f;
        public float MaxSpeed = 20;
        
    }
}