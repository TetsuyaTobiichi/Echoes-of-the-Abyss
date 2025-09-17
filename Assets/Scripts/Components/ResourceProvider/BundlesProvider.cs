// using System.Threading.Tasks;
// using UnityEngine;

// public class BundlesProvider : IResourceProvider
// {
//     public Task<T> LoadAsync<T>(string resourcePath) where T : Object
//     {
//         var tcs = new TaskCompletionSource<T>();
//         var request = AssetBundle.LoadAs(resourcePath);

//         if (!request.isDone)
//         {
//             request.completed += operation => onComplete();
//             return tcs.Task;
//         }

//         void onComplete()
//         {
//             if (request.asset is T asset)
//                 tcs.SetResult(asset);
//             else
//                 tcs.SetResult(null);
//         }

//         onComplete();

//         return tcs.Task;
//     }
// }
