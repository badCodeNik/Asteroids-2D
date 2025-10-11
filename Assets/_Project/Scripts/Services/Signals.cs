using _Project.Scripts.Player;

namespace _Project.Scripts.Services
{
    public class Signals
    {
        public class PlayerSpawnedSignal
        {
            public PlayerView PlayerView { get; }

            public PlayerSpawnedSignal(PlayerView playerView)
            {
                PlayerView = playerView;
            }
        }
    }
}