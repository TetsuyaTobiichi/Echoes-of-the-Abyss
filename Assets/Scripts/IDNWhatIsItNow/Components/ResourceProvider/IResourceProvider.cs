using System.Threading.Tasks;
using UnityEngine;

public interface IResourceProvider
{
    Task<T> LoadAsync<T>(string ResourcePath) where T : Object;
}
