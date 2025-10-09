using _Project.Scripts.Input;
using _Project.Scripts.Player;

namespace _Project.Scripts.Shooting
{
    public class ShootFeature
    {
        private readonly PlayerView _playerView;

        public ShootFeature(InputService inputService, PlayerView playerView)
        {
            _playerView = playerView;
            inputService.OnShoot += ShootBullet;
            inputService.OnShootLaser += ShootLaser;
        }

        private void ShootLaser()
        {
            
        }

        private void ShootBullet()
        {
            
        }
    }
}