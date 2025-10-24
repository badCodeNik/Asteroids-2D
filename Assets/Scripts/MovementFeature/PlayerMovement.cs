using _Project.Scripts.Configs;
using _Project.Scripts.Input;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Models;
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
        private readonly PlayerModel _playerModel;
        private PlayerView _playerView;
        private Vector2 _velocity;
        private float _desiredAngle;
        private float _currentSpeed;

        public PlayerMovement(InputService inputService, PlayerConfig playerConfig, SignalBus signalBus,
            WorldBoundsService worldBoundsService, PlayerModel playerModel)
        {
            _inputService = inputService;
            _playerConfig = playerConfig;
            _signalBus = signalBus;
            _worldBoundsService = worldBoundsService;
            _playerModel = playerModel;
            _signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetPlayer);
        }

        public void Tick()
        {
            if (!_playerView) return;
            HandleMovement();
            HandleRotation();

            ApplyVelocity();

            _playerView.transform.position = _worldBoundsService.WrapPosition(_playerView.transform.position);
        }

        private void ApplyVelocity()
        {
            _playerView.Position += _playerView.TotalVelocity * Time.deltaTime;
        }

        private void HandleRotation()
        {
            _desiredAngle += (-_inputService.MoveInput.x * _playerConfig.RotationSpeed * Time.deltaTime);
            _playerView.Rotation = _desiredAngle;
        }

        private void HandleMovement()
        {
            Vector2 input = _inputService.MoveInput;

            if (input.y != 0)
            {
                Vector2 forward = _playerView.transform.up;
                Vector2 acceleration = forward * (_playerConfig.Acceleration * input.y * Time.deltaTime);
                
                _playerView.Velocity += acceleration;
            }

            _playerView.Velocity *= (1f - _playerConfig.Drag * Time.deltaTime);

            if (_playerView.Velocity.magnitude > _playerConfig.MaxSpeed)
            {
                _playerView.Velocity = _playerView.Velocity.normalized * _playerConfig.MaxSpeed;
            }
            _playerModel.SetCoordinates(_playerView.Position);
            _playerModel.SetShipAngle(_playerView.Rotation);
            _playerModel.SetImmediateSpeed((int) _playerView.Velocity.magnitude);
        }


        private void SetPlayer(Signals.PlayerSpawnedSignal signal)
        {
            _playerView = signal.PlayerView.GetComponent<PlayerView>();
        }
    }
}