using _Project.Scripts.Input;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Models;
using _Project.Scripts.World;
using Zenject;

namespace _Project.Scripts.Shooting
{
    public class ShootFeature
    {
        private PlayerView _playerView;
        private readonly BulletSpawner _bulletSpawner;
        private readonly LaserSpawner _laserSpawner;
        private readonly WorldBoundsService _worldBoundsService;
        private readonly PlayerModel _playerModel;

        public ShootFeature(InputService inputService, BulletSpawner bulletSpawner, LaserSpawner laserSpawner,
            WorldBoundsService worldBoundsService, SignalBus signalBus, PlayerModel playerModel)
        {
            _bulletSpawner = bulletSpawner;
            _laserSpawner = laserSpawner;
            _worldBoundsService = worldBoundsService;
            _playerModel = playerModel;
            inputService.OnShoot += ShootBullet;
            inputService.OnShootLaser += ShootLaser;
            signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetPlayer);
            _laserSpawner.OnChargesChanged += _playerModel.SetLaserAmmo;
            _laserSpawner.OnRechargeTimerChanged += _playerModel.SetLaserCooldown;
        }

        private void SetPlayer(Signals.PlayerSpawnedSignal signal)
        {
            _playerView = signal.PlayerView.GetComponent<PlayerView>();
        }

        private void ShootLaser()
        {
            _laserSpawner.ShootLaser(_playerView.ShootPoint.position, _playerView.ShootPoint.up);
        }

        private void ShootBullet()
        {
            var bullet = _bulletSpawner.SpawnBullet(_playerView.ShootPoint);
            bullet.Initialize(_worldBoundsService);
        }
    }
}