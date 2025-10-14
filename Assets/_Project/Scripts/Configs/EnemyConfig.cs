using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public float SpawnRate = 1f;
        public float SpawnDelay;
        public int MaxEnemies;
        public float FlyingPlateSpeed;
        public float FlyingPlateAcceleration;
        public float FlyingPlateMaxSpeed;
        public float AsteroidSpeed;
        public float WorldSpawnOffset;
        public int AsteroidParticleCount;
        public float AsteroidParticleSpeed;
    }
}