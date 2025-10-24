using System;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    public interface IConfigData
    {
    }

    [Serializable]
    public class PlayerConfigData : IConfigData
    {
        public float acceleration = 5;
        public float rotationSpeed;
        public float drag = 0.98f;
        public float maxSpeed = 20;

        public int maxHealth = 3;
        public float invulnerabilityTime = 3f;
    }

    [Serializable]
    public class WorldConfigData : IConfigData
    {
        public float worldWidth = 1000f;
        public float worldHeight = 1000f;
    }

    [Serializable]
    public class EnemyConfigData : IConfigData
    {
        public float spawnRate = 1f;
        public float spawnDelay;
        public float flyingPlateSpeed;
        public float asteroidSpeed;
        public float worldSpawnOffset;
        public int asteroidParticleCount;
        public float asteroidParticleSpeed;
    }

    [Serializable]
    public class WeaponConfigData : IConfigData
    {
        public float speed;
        public float laserMaxDistance;
        public int maxCharges;
        public float rechargeTime;

        public float duration;
        
        public Color laserColor;
        public float laserWidth;
    }
}