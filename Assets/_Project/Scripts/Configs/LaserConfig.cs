using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LaserConfig", menuName = "Configs/LaserConfig", order = 0)]
    public class LaserConfig : ScriptableObject
    {
        public float MaxDistance;
        public int MaxCharges;
        public float RechargeTime;

        public float Duration;
        
        public Color LaserColor;
        public float LaserWidth;
    }
}