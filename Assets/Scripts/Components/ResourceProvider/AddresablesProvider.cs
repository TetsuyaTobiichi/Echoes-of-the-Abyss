using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddresablesProvider : IResourceProvider
{
    public Task<T> LoadAsync<T>(string resourcePath) where T : Object
    {
        return Addressables.LoadAssetAsync<T>(resourcePath).Task;
    }
}
