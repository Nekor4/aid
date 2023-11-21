using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Aid.Factory.Async
{
    public class ComponentAsyncFactory<T>: IAsyncFactory<T> where T : Component
    {
        private readonly AssetReferenceT<GameObject> _assetReference;
        private readonly Transform _parent;
        
        public ComponentAsyncFactory(AssetReferenceT<GameObject> assetReference, Transform parent = null)
        {
            _assetReference = assetReference;
            _parent = parent;
        }
        
        public async Task<T> Create()
        {
            if (_assetReference.IsDone == false)
            {
                await _assetReference.LoadSceneAsync().Task;
            }
            var instantiateAsync = _assetReference.InstantiateAsync(_parent);
            await instantiateAsync.Task;
            return instantiateAsync.Task.Result.GetComponent<T>();
        }
    }
}