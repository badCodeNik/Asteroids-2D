using System;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class AsteroidParticleView : MonoBehaviour, IDamageable
    {
        public event Action<AsteroidParticleView> OnAsteroidParticleShot;

        public void TakeDamage()
        {
            OnAsteroidParticleShot?.Invoke(this);
        }
    }
}