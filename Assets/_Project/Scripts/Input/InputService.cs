using System;
using _Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Input
{
    public class InputService : IInitializable, ITickable
    {
        private readonly Joystick _joystick;
        private readonly KeyBindingsConfig _keyBindingsConfig;
        private IInputHandler _inputHandler;
        public Vector2 MoveInput { get; private set; }
        public event Action OnShoot;
        public event Action OnShootLaser;
        private bool _isEnabled = true;

        public InputService(Joystick joystick, KeyBindingsConfig keyBindingsConfig)
        {
            _joystick = joystick;
            _keyBindingsConfig = keyBindingsConfig;
        }

        public void ToggleInput(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void Initialize()
        {
            if (Application.isMobilePlatform)
                _inputHandler = new MobileInput(_joystick);
            else
                _inputHandler = new DesktopInput(_keyBindingsConfig);
        }

        public void Tick()
        {
            if (!_isEnabled) return;
            MoveInput = _inputHandler.GetMovement();
            if (_inputHandler.IsShooting()) OnShoot?.Invoke();
            if (_inputHandler.IsShootingLaser()) OnShootLaser?.Invoke();
        }
    }
}