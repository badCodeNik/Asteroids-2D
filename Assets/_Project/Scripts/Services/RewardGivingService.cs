using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class RewardGivingService
    {
        private readonly Dictionary<EnemyType, int> _rewardsForTypes;

        public RewardGivingService(SignalBus signalBus)
        {
            signalBus.Subscribe<Signals.EnemyKilledSignal>(GiveReward);
            _rewardsForTypes = new Dictionary<EnemyType, int>()
            {
                { EnemyType.Asteroid, 10 },
                { EnemyType.FlyingPlate, 15 },
                { EnemyType.AsteroidParticle, 20 },
            };
        }

        private void GiveReward(Signals.EnemyKilledSignal signal)
        {
            if (_rewardsForTypes.TryGetValue(signal.Type, out int value))
            {
                //Выдаём ревард
                Debug.Log($"Reward: {value}");
            }
        }
    }
}