using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Aid.Pool.Async
{
    [Serializable]
    public class SerializableGameObjectAsyncPool
    {
        [SerializeField] private AssetReferenceT<GameObject> reference;

        private GameObjectAsyncPool _pool;

        private GameObjectAsyncPool Pool => _pool ??= GameObjectsAsyncPoolsRoot.Instance.GetPool(reference);

        public async Task<GameObject> Request()
        {
            return await Pool.Request();
        }
        
        public void Request(Action<GameObject> callback)
        {
            Pool.Request( gameObject =>
            {
                gameObject.SetActive(true);
                callback?.Invoke(gameObject);
            });
        }

        public void Request(int number, Action<IEnumerable<GameObject>> callback)
        {
            Pool.Request(number, objects =>
            {
                foreach (var gameObject in objects)
                {
                    gameObject.SetActive(true);
                }
                
                callback?.Invoke(objects);
            });
        }
        

        public void Return(GameObject member)
        {
            member.SetActive(false);
            Pool.Return(member);
        }
    }
}