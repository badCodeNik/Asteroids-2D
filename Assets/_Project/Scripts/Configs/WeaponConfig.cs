using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        public float Speed;
        public float LaserMaxDistance;
        public int MaxCharges;
        public float RechargeTime;

        public float Duration;
        
        public Color LaserColor;
        public float LaserWidth;
        
        
        
        public void LoadFromData(WeaponConfigData data)
        {
            Speed = data.speed;
            LaserMaxDistance = data.laserMaxDistance;
            MaxCharges = data.maxCharges;
            RechargeTime = data.rechargeTime;
            Duration = data.duration;
            LaserColor = data.laserColor;
            LaserWidth = data.laserWidth;
        }
        
        public WeaponConfigData ToData()
        {
            return new WeaponConfigData
            {
                speed = Speed,
                laserMaxDistance = LaserMaxDistance,
                maxCharges = MaxCharges,
                rechargeTime = RechargeTime,
                duration = Duration,
                laserColor = LaserColor,
                laserWidth = LaserWidth
            };
        }
    }
}