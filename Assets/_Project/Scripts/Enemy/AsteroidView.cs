using System;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class AsteroidView : MonoBehaviour, IDamageable
    {
        public event Action<AsteroidView> OnAsteroidShot;
        public Vector2 Direction { get; private set; }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void TakeDamage()
        {
            OnAsteroidShot?.Invoke(this);
        }
    }
}