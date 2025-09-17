using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

public class WebResourceProvider : IResourceProvider
{
    private const string _baseUrl = "";
    public Task<T> LoadAsync<T>(string resourcePath) where T : Object
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_baseUrl + GetPrefabName(resourcePath));
        UnityWebRequestAsyncOperation operation = request.SendWebRequest();

        TaskCompletionSource<T> tcs = new();

        if (!operation.isDone)
        {
            operation.completed += (op) =>
            {

            };
        }

        void onComplete()
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load {resourcePath} from server: {request.error}");
                tcs.SetResult(null);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

                T asset = bundle.LoadAsset<T>(resourcePath);
                tcs.SetResult(asset);
            }
        }

        return tcs.Task;
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
