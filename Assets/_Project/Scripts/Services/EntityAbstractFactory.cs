using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public abstract class EntityAbstractFactory<T> where T : Component
    {
        protected readonly DiContainer Container;
        protected readonly ResourceLoadingService ResourceLoadingService;
        protected ObjectPool<T> Pool; 

        protected EntityAbstractFactory(
            DiContainer container,
            ResourceLoadingService resourceLoadingService)
        {
            Container = container;
            ResourceLoadingService = resourceLoadingService;
        }

        public abstract T Create();
    }
}