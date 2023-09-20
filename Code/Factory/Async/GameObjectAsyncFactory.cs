using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Aid.Factory
{
    public class GameObjectAsyncFactory : IAsyncFactory<GameObject>
    {
        private AssetReference assetReference;
        private Transform parent;
        
        public GameObjectAsyncFactory(AssetReferenceT<GameObject> assetReference, Transform parent = null)
        {
            this.assetReference = assetReference;
            this.parent = parent;
        }
        
        public async Task<GameObject> Create()
        {
            if (assetReference.IsDone == false)
            {
                await assetReference.LoadSceneAsync().Task;
            }
            var instantiateAsync = assetReference.InstantiateAsync(parent);
            await instantiateAsync.Task;
            return instantiateAsync.Task.Result;
        }
    }
}