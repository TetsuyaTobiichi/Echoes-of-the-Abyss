using UnityEngine;

namespace Components.PoolingManager
{
    public interface IPoolingManager
    {
        void KeepHandle(GameObject gameObject);
        void Release(GameObject gameObject);
        void ClearFromCache(GameObject gameObject);
    }
}