using UnityEngine;

namespace _Project.Scripts.Services
{
    public class ResourceLoadingService
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}