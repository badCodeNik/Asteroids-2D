using UnityEngine;
using Zenject;

namespace _Project.Scripts.Configs
{
    public class ConfigProvider : IInitializable
    {
        private readonly ConfigLoader _configLoader;

        private readonly PlayerConfig _playerConfig;
        private readonly EnemyConfig _enemyConfig;
        private readonly WorldConfig _worldConfig;
        private readonly WeaponConfig _laserConfig;

        public ConfigProvider(ConfigLoader configLoader,
            PlayerConfig playerConfig,
            EnemyConfig enemyConfig,
            WorldConfig worldConfig,
            WeaponConfig laserConfig)
        {
            _configLoader = configLoader;
            _playerConfig = playerConfig;
            _enemyConfig = enemyConfig;
            _worldConfig = worldConfig;
            _laserConfig = laserConfig;
        }

        public void Initialize()
        {
            LoadAllConfigs();
        }

        public void SaveAllConfigs()
        {
            _configLoader.SaveConfig("PlayerConfig.json", _playerConfig.ToData());
            _configLoader.SaveConfig("EnemyConfig.json", _enemyConfig.ToData());
            _configLoader.SaveConfig("WorldConfig.json", _worldConfig.ToData());
            _configLoader.SaveConfig("WeaponConfig.json", _laserConfig.ToData());

            Debug.Log("All configs saved to JSON!");
        }

        private void LoadAllConfigs()
        {
            var playerData = _configLoader.LoadConfig<PlayerConfigData>("PlayerConfig.json");
            var enemyData = _configLoader.LoadConfig<EnemyConfigData>("EnemyConfig.json");
            var worldData = _configLoader.LoadConfig<WorldConfigData>("WorldConfig.json");
            var laserData = _configLoader.LoadConfig<WeaponConfigData>("WeaponConfig.json");

            _playerConfig.LoadFromData(playerData);
            _enemyConfig.LoadFromData(enemyData);
            _worldConfig.LoadFromData(worldData);
            _laserConfig.LoadFromData(laserData);

            Debug.Log("All configs loaded from JSON!");
        }
    }
}