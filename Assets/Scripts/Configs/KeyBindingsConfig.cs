using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "KeyBindingsConfig", menuName = "Configs/KeyBindingsConfig")]
    public class KeyBindingsConfig : ScriptableObject
    {
        public KeyCode ShootBullet;
        public KeyCode ShootLaser;
    }
}