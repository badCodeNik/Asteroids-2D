using System;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private PlayerFactory _playerFactory;


        [Inject]
        private void Construct(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private void Start()
        {
            _playerFactory.SpawnPlayer();
        }
    }
}