using _Project.Scripts.Configs;
using _Project.Scripts.Input;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MovementFeature
{
    public class PlayerMovement : ITickable
    {
        private readonly InputService _inputService;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerView _playerView;
        private Vector2 _velocity;

        public PlayerMovement(InputService inputService, PlayerConfig playerConfig, PlayerView playerView)
        {
            _inputService = inputService;
            _playerConfig = playerConfig;
            _playerView = playerView;
        }

        public void Tick()
        {
            _velocity += _playerConfig.Acceleration * _inputService.MoveInput * Time.deltaTime;
            _velocity *= _playerConfig.Drag;
            if(_velocity.magnitude > _playerConfig.MaxSpeed)
                _velocity = _velocity.normalized * _playerConfig.MaxSpeed;

            _playerView.transform.position += (Vector3)_velocity * Time.deltaTime;
        }
    }
}