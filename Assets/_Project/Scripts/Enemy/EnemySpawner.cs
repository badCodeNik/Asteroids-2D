using System;
using System.Threading;
using _Project.Scripts.Configs;
using _Project.Scripts.MovementFeature;
using _Project.Scripts.Physics;
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
        private readonly ICustomPhysicsService _customPhysics;
        private readonly SignalBus _signalBus;
        private CancellationTokenSource _cts = new();
        private Transform _target;

        public EnemySpawner(EnemyConfig enemyConfig, AsteroidFactory asteroidFactory,
            AsteroidParticleFactory asteroidParticleFactory,
            FlyingPlateFactory flyingPlateFactory, TimerService timerService, StrategyMoveAgent moveAgent,
            ICustomPhysicsService customPhysics,
            SignalBus signalBus)
        {
            _enemyConfig = enemyConfig;
            _asteroidFactory = asteroidFactory;
            _asteroidParticleFactory = asteroidParticleFactory;
            _flyingPlateFactory = flyingPlateFactory;
            _timerService = timerService;
            _moveAgent = moveAgent;
            _customPhysics = customPhysics;
            _signalBus = signalBus;
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
            _customPhysics.RegisterBody(flyingPlate);
            flyingPlate.OnFlyingPlateShot += DestroyPlate;
            _moveAgent.AddMoveSubject(flyingPlate,
                new FollowPlayerMoveStrategy(_target, _enemyConfig.FlyingPlateSpeed), _target);
        }

        private void DestroyPlate(FlyingPlateView plate)
        {
            _signalBus.Fire(new Signals.EnemyKilledSignal(plate.Type));
            _customPhysics.UnregisterBody(plate);
            _flyingPlateFactory.Release(plate);
            _moveAgent.RemoveMoveSubject(plate);
        }

        private void SpawnAsteroid()
        {
            var asteroid = _asteroidFactory.Create();
            _customPhysics.RegisterBody(asteroid);
            asteroid.OnAsteroidShot += SplitAsteroid;
            asteroid.OnAsteroidDestroyed += DestroyAsteroid;
            _moveAgent.AddMoveSubject(asteroid,
                new StraightMoveStrategy(_enemyConfig.AsteroidSpeed, asteroid.Direction.normalized));
        }

        private void DestroyAsteroid(AsteroidView asteroid)
        {
            _signalBus.Fire(new Signals.EnemyKilledSignal(asteroid.Type));
            _customPhysics.UnregisterBody(asteroid);
            _moveAgent.RemoveMoveSubject(asteroid);
            _asteroidFactory.Release(asteroid);
        }

        private void SplitAsteroid(AsteroidView asteroid)
        {
            for (int i = 0; i < _enemyConfig.AsteroidParticleCount; i++)
            {
                var particle = _asteroidParticleFactory.Create();
                particle.OnAsteroidParticleShot += DestroyParticle;
                _customPhysics.RegisterBody(particle);
                particle.transform.position = asteroid.transform.position;
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                _moveAgent.AddMoveSubject(particle,
                    new StraightMoveStrategy(_enemyConfig.AsteroidParticleSpeed, randomDirection));
            }

            DestroyAsteroid(asteroid);
        }

        private void DestroyParticle(AsteroidParticleView particle)
        {
            _asteroidParticleFactory.Release(particle);
            _customPhysics.UnregisterBody(particle);
            _moveAgent.RemoveMoveSubject(particle);
            _signalBus.Fire(new Signals.EnemyKilledSignal(particle.Type));
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