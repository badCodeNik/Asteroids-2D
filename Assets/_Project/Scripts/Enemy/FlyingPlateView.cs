using System;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class FlyingPlateView : MonoBehaviour, IDamageable
    {
        public event Action<FlyingPlateView> OnFlyingPlateShot;
        public void TakeDamage(bool destroyImmediately = false)
        {
            OnFlyingPlateShot?.Invoke(this);
        }
    }
}