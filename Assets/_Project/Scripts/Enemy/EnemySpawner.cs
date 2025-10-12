using System;
using System.Threading;
using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Enemy
{
    public class EnemySpawner : IDisposable, IInitializable
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly AsteroidParticleFactory _asteroidParticleFactory;
        private readonly FlyingPlateFactory _flyingPlateFactory;
        private readonly TimerService _timerService;
        private readonly StrategyMoveAgent _moveAgent;
        private CancellationTokenSource _cts = new();
        private Transform _target;

        public EnemySpawner(EnemyConfig enemyConfig, AsteroidFactory asteroidFactory, AsteroidParticleFactory asteroidParticleFactory,
            FlyingPlateFactory flyingPlateFactory, TimerService timerService, StrategyMoveAgent moveAgent, SignalBus signalBus)
        {
            _enemyConfig = enemyConfig;
            _asteroidFactory = asteroidFactory;
            _asteroidParticleFactory = asteroidParticleFactory;
            _flyingPlateFactory = flyingPlateFactory;
            _timerService = timerService;
            _moveAgent = moveAgent;
            signalBus.Subscribe<Signals.PlayerSpawnedSignal>(SetTarget);
        }

        private void SetTarget(Signals.PlayerSpawnedSignal signal)
        {
            _target = signal.PlayerView;
        }

        private async void StartSpawningWithDelay(float delaySeconds)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds));
            _timerService.StartLoopTimer(_enemyConfig.SpawnRate, SpawnAsteroid, _cts.Token).Forget();
            _timerService.StartLoopTimer(_enemyConfig.SpawnRate, SpawnFlyingPlate, _cts.Token).Forget();
        }

        private void SpawnFlyingPlate()
        {
            var flyingPlate = _flyingPlateFactory.Create();
            _moveAgent.AddMoveSubject(flyingPlate.gameObject, new FollowPlayerMoveStrategy(_target, _enemyConfig.FlyingPlateSpeed), _target);
        }

        private void SpawnAsteroid()
        {
            var asteroid = _asteroidFactory.Create();
            asteroid.OnAsteroidShot += DestroyAsteroid;
            _moveAgent.AddMoveSubject(asteroid.gameObject, new StraightMoveStrategy(_enemyConfig.AsteroidSpeed, asteroid.Direction.normalized));
        }

        private void DestroyAsteroid(AsteroidView asteroid)
        {
            _moveAgent.RemoveMoveSubject(asteroid.gameObject);
            for(int i = 0; i < _enemyConfig.AsteroidParticleCount; i++)
            {
                var particle = _asteroidParticleFactory.Create();
                particle.transform.position = asteroid.transform.position;
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                _moveAgent.AddMoveSubject(particle.gameObject, new StraightMoveStrategy(_enemyConfig.AsteroidParticleSpeed, randomDirection));
            }

            _asteroidFactory.Release(asteroid);
        }

        public void Dispose()
        {
            _cts.Cancel();
        }

        public void Initialize()
        {
            StartSpawningWithDelay(_enemyConfig.SpawnDelay);
        }
    }
}