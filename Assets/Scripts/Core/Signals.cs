using UnityEngine;

namespace _Project.Scripts.Services
{
    public class Signals
    {
        public class PlayerSpawnedSignal
        {
            public Transform PlayerView { get; }

            public PlayerSpawnedSignal(Transform playerView)
            {
                PlayerView = playerView;
            }
        }

        public class EnemyKilledSignal
        {
            public EnemyType Type { get; }

            public EnemyKilledSignal(EnemyType type)
            {
                Type = type;
            }
        }
    }
}