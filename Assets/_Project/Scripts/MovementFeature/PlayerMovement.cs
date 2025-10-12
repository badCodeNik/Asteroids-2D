using _Project.Scripts.Configs;
using _Project.Scripts.Input;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.World;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MovementFeature
{
    public class PlayerMovement : ITickable
    {
        private readonly InputService _inputService;
        private readonly PlayerConfig _playerConfig;
        private readonly SignalBus _signalBus;
        private readonly WorldBoundsService _worldBoundsService;
        private PlayerView _playerView;
        private Vector2 _velocity;
        private float _desiredAngle;
        private float _currentSpeed;

        public PlayerMovement(InputService inputService, PlayerConfig playerConfig, SignalBus signalBus,
            WorldBoundsService worldBoundsService)
        {
            _inputService = inputService;
            _playerConfig = playerConfig;
            _signalBus = signalBus;
            _worldBoundsService = worldBoundsService;
            _signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetPlayer);
        }

        public void Tick()
        {
            if (!_playerView) return;
            HandleMovement();
            HandleRotation();

            _playerView.transform.position = _worldBoundsService.WrapPosition(_playerView.transform.position);
        }

        private void HandleRotation()
        {
            _desiredAngle += (-_inputService.MoveInput.x * _playerConfig.RotationSpeed * Time.deltaTime);
            _playerView.transform.rotation = Quaternion.Euler(0, 0, _desiredAngle);
        }

        private void HandleMovement()
        {
            _currentSpeed += _playerConfig.Acceleration * _inputService.MoveInput.y * Time.deltaTime;
            _currentSpeed *= _playerConfig.Drag;

            if (_currentSpeed > _playerConfig.MaxSpeed)
                _currentSpeed = _playerConfig.MaxSpeed;
            else if (_currentSpeed < -_playerConfig.MaxSpeed)
                _currentSpeed = -_playerConfig.MaxSpeed;

            _playerView.transform.position += _playerView.transform.up * _currentSpeed * Time.deltaTime;
        }


        private void SetPlayer(Signals.PlayerSpawnedSignal signal)
        {
            _playerView = signal.PlayerView.GetComponent<PlayerView>();
        }
    }
}