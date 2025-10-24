using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public abstract class EntityAbstractFactory<T> where T : Component
    {
        protected ObjectPool<T> Pool; 


        public abstract T Create();
    }
}