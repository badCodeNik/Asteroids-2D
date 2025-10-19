using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public float SpawnRate = 1f;
        public float SpawnDelay;
        public float FlyingPlateSpeed;
        public float AsteroidSpeed;
        public float WorldSpawnOffset;
        public int AsteroidParticleCount;
        public float AsteroidParticleSpeed;
        
        
        public void LoadFromData(EnemyConfigData data)
        {
            SpawnRate = data.spawnRate;
            SpawnDelay = data.spawnDelay;
            FlyingPlateSpeed = data.flyingPlateSpeed;
            AsteroidSpeed = data.asteroidSpeed;
            WorldSpawnOffset = data.worldSpawnOffset;
            AsteroidParticleCount = data.asteroidParticleCount;
            AsteroidParticleSpeed = data.asteroidParticleSpeed;
        }
        
        public EnemyConfigData ToData()
        {
            return new EnemyConfigData
            {
                spawnRate = SpawnRate,
                spawnDelay = SpawnDelay,
                flyingPlateSpeed = FlyingPlateSpeed,
                asteroidSpeed = AsteroidSpeed,
                worldSpawnOffset = WorldSpawnOffset,
                asteroidParticleCount = AsteroidParticleCount,
                asteroidParticleSpeed = AsteroidParticleSpeed
            };
        }
    }
}