using _Project.Scripts.UI.Models;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.ViewModels
{
    public class PlayerHUDViewModel
    {
        public IReadOnlyReactiveProperty<Vector2> Coordinates => _model.Coordinates;
        public IReadOnlyReactiveProperty<float> ShipAngle => _model.ShipAngle;
        public IReadOnlyReactiveProperty<int> LaserAmmo => _model.LaserAmmo;
        public IReadOnlyReactiveProperty<float> LaserCooldown => _model.LaserCooldown;
        public IReadOnlyReactiveProperty<int> ImmediateSpeed => _model.ImmediateSpeed;
        public IReadOnlyReactiveProperty<int> Health => _model.Health;
        private readonly PlayerModel _model;

        public PlayerHUDViewModel(PlayerModel model)
        {
            _model = model;
        }
    }
}