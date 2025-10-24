using _Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.World
{
    public class WorldBoundsService
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly SignalBus _signalBus;
        private readonly float _maxY;
        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;

        public bool IsOutOfBounds(Vector2 position) =>
            position.x > _maxX || position.x < _minX || position.y > _maxY || position.y < _minY;

        public WorldBoundsService(WorldConfig worldConfig, EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
            _minX = -worldConfig.WorldWidth / 2f;
            _maxX = worldConfig.WorldWidth / 2f;
            _minY = -worldConfig.WorldHeight / 2f;
            _maxY = worldConfig.WorldHeight / 2f;
        }

        public Vector2 WrapPosition(Vector2 position)
        {
            if (position.x > _maxX)
                position.x = _minX + (position.x - _maxX);
            else if (position.x < _minX)
                position.x = _maxX + (position.x - _minX);

            if (position.y > _maxY)
                position.y = _minY + (position.y - _maxY);
            else if (position.y < _minY)
                position.y = _maxY + (position.y - _minY);

            return position;
        }

        public Vector2 GetRandomOutOfWorldPosition()
        {
            float offset = _enemyConfig.WorldSpawnOffset;
    
            int side = Random.Range(0, 4);
    
            switch (side)
            {
                case 0:
                    return new Vector2(
                        Random.Range(_minX, _maxX),
                        _maxY + Random.Range(0, offset)
                    );
            
                case 1:
                    return new Vector2(
                        _maxX + Random.Range(0, offset),      
                        Random.Range(_minY, _maxY)           
                    );
            
                case 2:
                    return new Vector2(
                        Random.Range(_minX, _maxX),           
                        _minY - Random.Range(0, offset)
                    );

                default:
                    return new Vector2(
                        _minX - Random.Range(0, offset),
                        Random.Range(_minY, _maxY)
                    );
            }
        }        
        public Vector2 GetRandomInWorldPosition()
        {
            return new Vector2(
                Random.Range(_minX, _maxX),
                Random.Range(_minY, _maxY));
        }
    }
}