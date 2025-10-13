using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Input
{
    public class DesktopInput : IDesktopInput
    {
        private readonly KeyBindingsConfig _keyBindingsConfig;

        public DesktopInput(KeyBindingsConfig keyBindingsConfig)
        {
            _keyBindingsConfig = keyBindingsConfig;
        }

        public Vector2 GetMovement()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }

        public bool IsShooting()
        {
            return UnityEngine.Input.GetKeyDown(_keyBindingsConfig.ShootBullet);
        }

        public bool IsShootingLaser()
        {
            return UnityEngine.Input.GetKeyDown(_keyBindingsConfig.ShootLaser);
        }

    }
}