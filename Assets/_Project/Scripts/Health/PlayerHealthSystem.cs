using System;
using _Project.Scripts.Configs;
using _Project.Scripts.Input;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Health
{
    public class PlayerHealthSystem : IInitializable, IDisposable
    {
        private PlayerConfig _playerConfig;
        private readonly InputService _inputService;
        private PlayerView _playerView;
        private int _currentHealth;
        private bool _isInvulnerable;
        private event Action<int> OnHealthChanged;

        public bool IsDead => _currentHealth <= 0;
        public PlayerHealthSystem(SignalBus signalBus,PlayerConfig playerConfig, InputService inputService)
        {
            _playerConfig = playerConfig;
            _inputService = inputService;
            signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetPlayer);
        }

        private void SetPlayer(Signals.PlayerSpawnedSignal signal)
        {
            _playerView = signal.PlayerView.GetComponent<PlayerView>();
            _playerView.OnDamageTaken += TakeDamage;
        }

        private void TakeDamage()
        {
            if(_isInvulnerable) return;
            _currentHealth--;
            MakeTemporaryInvulnerable().Forget();
            OnHealthChanged?.Invoke(_currentHealth);
        }

        private async UniTask MakeTemporaryInvulnerable()
        {
            _isInvulnerable = true;
            _playerView.SetCanCollide(false);
            _playerView.EnableParticles(true);
            _inputService.ToggleInput(false);
            await UniTask.Delay(TimeSpan.FromSeconds(_playerConfig.InvulnerabilityTime));
            _isInvulnerable = false;
            _inputService.ToggleInput(true);
            _playerView.EnableParticles(false);
            _playerView.SetCanCollide(true);
        }

        public void Initialize()
        {
            _currentHealth = _playerConfig.MaxHealth;
        }

        public void Dispose()
        {
            
        }
        
    }
}