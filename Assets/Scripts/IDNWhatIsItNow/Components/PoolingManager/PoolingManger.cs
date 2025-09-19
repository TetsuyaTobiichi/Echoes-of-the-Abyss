
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Components.PoolingManager
{
    public class PoolingManager : MonoBehaviour, IPoolingManager
    {
        private Dictionary<string, object> _cache;
        private IResourceProvider _resourceProvider;
        [SerializeField]
        Transform CacheObject;

        public PoolingManager(IResourceProvider resourceProvider)
        {
            this._resourceProvider = resourceProvider;
        }

        void Awake()
        {
            DontDestroyOnLoad(CacheObject);
        }

        public async Task<T> GetResource<T>(string resourceKey) where T : Object
        {
            if (_cache.TryGetValue(GetPrefabName(resourceKey), out object obj))
            {
                return obj as T;
            }

            var gameObject = await _resourceProvider.LoadAsync<T>(resourceKey);

            return gameObject as T;
        }

        public void KeepHandle(GameObject gameObject)
        {
            if (!_cache.TryGetValue(nameof(gameObject), out object obj))
            {
                _cache.Add(nameof(gameObject), gameObject);
            }
        }

        public void Release(GameObject gameObject)
        {
            if (_cache.TryGetValue(nameof(gameObject), out object obj))
            {
                gameObject.transform.SetParent(CacheObject);
                gameObject.SetActive(false);
            }
        }

        public void ClearFromCache(GameObject gameObject)
        {
            if (_cache.TryGetValue(nameof(gameObject), out object obj))
            {
                _cache.Remove(nameof(obj));
                Destroy(gameObject);
            }
        }

        public static string GetPrefabName(string resourcePath)
        {
            if (string.IsNullOrEmpty(resourcePath))
                return null;

            string[] parts = resourcePath.Split('/');

            string fileName = parts[parts.Length - 1];

            int dotIndex = fileName.LastIndexOf('.');
            if (dotIndex >= 0)
                fileName = fileName.Substring(0, dotIndex);

            return fileName;
        }
    }
}