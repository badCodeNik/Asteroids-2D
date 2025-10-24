using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float Acceleration = 5;
        public float RotationSpeed;
        public float Drag = 0.98f;
        public float MaxSpeed = 20;
        
        [Header("Health")]
        public int MaxHealth = 3;
        public float InvulnerabilityTime = 3f;
        
        public void LoadFromData(PlayerConfigData data)
        {
            Acceleration = data.acceleration;
            RotationSpeed = data.rotationSpeed;
            Drag = data.drag;
            MaxSpeed = data.maxSpeed;
            MaxHealth = data.maxHealth;
            InvulnerabilityTime = data.invulnerabilityTime;
        }

        public PlayerConfigData ToData()
        {
            return new PlayerConfigData
            {
                acceleration = Acceleration,
                rotationSpeed = RotationSpeed,
                drag = Drag,
                maxSpeed = MaxSpeed,
                maxHealth = MaxHealth,
                invulnerabilityTime = InvulnerabilityTime,
            };
        }
        
    }
}