#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using _Project.Scripts.Configs;

namespace _Project.Editor
{
    public class ConfigExporter : EditorWindow
    {
        private PlayerConfig _playerConfig;
        private EnemyConfig _enemyConfig;
        private WorldConfig _worldConfig;
        private WeaponConfig _weaponConfig;

        [MenuItem("Tools/Config Exporter")]
        public static void ShowWindow()
        {
            GetWindow<ConfigExporter>("Config Exporter");
        }

        private void OnGUI()
        {
            GUILayout.Label("Export Configs to JSON", EditorStyles.boldLabel);

            _playerConfig =
                (PlayerConfig)EditorGUILayout.ObjectField("Player Config", _playerConfig, typeof(PlayerConfig), false);
            _enemyConfig =
                (EnemyConfig)EditorGUILayout.ObjectField("Enemy Config", _enemyConfig, typeof(EnemyConfig), false);
            _worldConfig =
                (WorldConfig)EditorGUILayout.ObjectField("World Config", _worldConfig, typeof(WorldConfig), false);
            _weaponConfig =
                (WeaponConfig)EditorGUILayout.ObjectField("Weapon Config", _weaponConfig, typeof(WeaponConfig), false);

            if (GUILayout.Button("Export All to JSON"))
            {
                ExportConfigs();
            }
        }

        private void ExportConfigs()
        {
            var loader = new ConfigLoader();

            if (_playerConfig != null)
                loader.SaveConfig("PlayerConfig.json", _playerConfig.ToData());

            if (_enemyConfig != null)
                loader.SaveConfig("EnemyConfig.json", _enemyConfig.ToData());

            if (_worldConfig != null)
                loader.SaveConfig("WorldConfig.json", _worldConfig.ToData());

            if (_weaponConfig != null)
                loader.SaveConfig("WeaponConfig.json", _weaponConfig.ToData());

            Debug.Log("All configs exported to StreamingAssets/Configs/");
            AssetDatabase.Refresh();
        }
    }
}
#endif