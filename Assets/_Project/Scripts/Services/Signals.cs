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
    }
}