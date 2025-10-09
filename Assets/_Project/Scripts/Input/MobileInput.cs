using UnityEngine;

namespace _Project.Scripts.Input
{
    public class MobileInput : IMobileInput
    {
        private readonly Joystick _joystick;

        public MobileInput(Joystick joystick)
        {
            _joystick = joystick;
        }
        public Vector2 GetMovement()
        {
            return _joystick.GetInputVector();
        }

        public bool IsShooting()
        {
            return _joystick.ShootPressed;
        }

        public bool IsShootingLaser()
        {
            return _joystick.ShootLaserPressed;
        }
        
    }
}