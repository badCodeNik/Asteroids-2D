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

            if (GUILayout.Button("Find configs"))
            {
                FindConfigs();
            }
        }

        private void FindConfigs()
        {
            _playerConfig = FindConfigOfType<PlayerConfig>();
            _enemyConfig = FindConfigOfType<EnemyConfig>();
            _worldConfig = FindConfigOfType<WorldConfig>();
            _weaponConfig = FindConfigOfType<WeaponConfig>();

            int foundCount = 0;
            if (_playerConfig != null) foundCount++;
            if (_enemyConfig != null) foundCount++;
            if (_worldConfig != null) foundCount++;
            if (_weaponConfig != null) foundCount++;

            Debug.Log($"Found {foundCount}/4 configs automatically!");

            if (_playerConfig != null) Debug.Log($"✓ PlayerConfig found: {AssetDatabase.GetAssetPath(_playerConfig)}");
            if (_enemyConfig != null) Debug.Log($"✓ EnemyConfig found: {AssetDatabase.GetAssetPath(_enemyConfig)}");
            if (_worldConfig != null) Debug.Log($"✓ WorldConfig found: {AssetDatabase.GetAssetPath(_worldConfig)}");
            if (_weaponConfig != null) Debug.Log($"✓ WeaponConfig found: {AssetDatabase.GetAssetPath(_weaponConfig)}");

            Repaint();
        }

        private T FindConfigOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            if (guids.Length == 0)
            {
                Debug.LogWarning($"{typeof(T).Name} not found in project!");
                return null;
            }

            if (guids.Length > 1)
            {
                Debug.LogWarning($"Multiple {typeof(T).Name} found! Using the first one.");
            }

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            T config = AssetDatabase.LoadAssetAtPath<T>(path);

            return config;
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