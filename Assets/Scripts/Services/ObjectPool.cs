using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly int _maxSize;
        private readonly Queue<T> _pool = new();

        public ObjectPool(T prefab, Transform parent = null, int defaultCapacity = 10, int maxSize = 50)
        {
            _prefab = prefab;
            _parent = parent;
            _maxSize = maxSize;

            for (int i = 0; i < defaultCapacity; i++)
            {
                CreateInstance();
            }
        }

        private T CreateInstance()
        {
            T instance = Object.Instantiate(_prefab, _parent);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
            return instance;
        }

        private T CreateInstance(Vector3 position)
        {
            T instance = Object.Instantiate(_prefab, position, Quaternion.identity, _parent);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
            return instance;
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                if (_pool.Count >= _maxSize)
                {
                    Debug.LogWarning($"[{typeof(T)} Pool] Max size reached.");
                    return null;
                }

                CreateInstance();
            }

            T instance = _pool.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }


        public void Release(T instance)
        {
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_parent);
            _pool.Enqueue(instance);
        }

        public void Clear()
        {
            foreach (var obj in _pool)
            {
                Object.Destroy(obj.gameObject);
            }

            _pool.Clear();
        }

        public T Get(Vector3 position)
        {
            if (_pool.Count == 0)
            {
                if (_pool.Count >= _maxSize)
                {
                    Debug.LogWarning($"[{typeof(T)} Pool] Max size reached.");
                    return null;
                }

                CreateInstance(position);
            }

            T instance = _pool.Dequeue();
            instance.transform.position = position;
            instance.gameObject.SetActive(true);
            return instance;
        }
    }
}