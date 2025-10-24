using System.IO;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    public class ConfigLoader
    {
        private const string CONFIG_FOLDER = "Configs";

        public T LoadConfig<T>(string fileName) where T : IConfigData
        {
            string path = GetConfigPath(fileName);

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Config file not found: {path}. Creating default config.");
                CreateDefaultConfig<T>(path);
            }

            string json = File.ReadAllText(path);
            T config = JsonUtility.FromJson<T>(json);

            return config;
        }

        public void SaveConfig<T>(string fileName, T data) where T : IConfigData
        {
            string path = GetConfigPath(fileName);
            string json = JsonUtility.ToJson(data, true);

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, json);
            Debug.Log($"Saved config: {fileName} to {path}");
        }

        private string GetConfigPath(string fileName)
        {
            return Path.Combine(Application.streamingAssetsPath, CONFIG_FOLDER, fileName);
        }

        private void CreateDefaultConfig<T>(string path) where T : IConfigData
        {
            T defaultConfig = System.Activator.CreateInstance<T>();
            string json = JsonUtility.ToJson(defaultConfig, true);

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, json);
        }
    }
}