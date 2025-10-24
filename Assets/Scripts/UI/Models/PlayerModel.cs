using System;
using _Project.Scripts.Configs;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Models
{
    public class PlayerModel
    {
        public ReactiveProperty<int> Health { get; private set; } = new();
        public ReactiveProperty<Vector2> Coordinates { get; private set; } = new();
        public ReactiveProperty<float> ShipAngle { get; private set; } = new();
        public ReactiveProperty<int> ImmediateSpeed { get; private set; } = new();
        public ReactiveProperty<int> LaserAmmo { get; private set; } = new();
        public ReactiveProperty<float> LaserCooldown { get; private set; } = new();

        public PlayerModel(PlayerConfig playerConfig,WeaponConfig config)
        {
            SetHealth(playerConfig.MaxHealth);
            SetLaserAmmo(config.MaxCharges);
            SetLaserCooldown(config.RechargeTime);
        }
        
        public void SetHealth(int health)
        {
            Health.Value = Math.Max(0,health);
        }
        public void SetCoordinates(Vector2 coordinates)
        {
            Coordinates.Value = coordinates;
        }
        public void SetShipAngle(float shipAngle)
        {
            ShipAngle.Value = shipAngle;
        }
        
        public void SetImmediateSpeed(int immediateSpeed)
        {
            ImmediateSpeed.Value = immediateSpeed;
        }
        public void SetLaserAmmo(int laserAmmo)
        {
            LaserAmmo.Value = laserAmmo;
        }
        
        public void SetLaserCooldown(float laserCooldown)
        {
            LaserCooldown.Value = MathF.Max(0,laserCooldown);
        }
    }
}