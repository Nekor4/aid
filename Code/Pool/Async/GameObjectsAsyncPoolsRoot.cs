using System.Collections.Generic;
using Aid.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Aid.Pool
{
    public class GameObjectsAsyncPoolsRoot : MonoBehaviour
    {
        private static GameObjectsAsyncPoolsRoot _instance;

        public static GameObjectsAsyncPoolsRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Game Object Pool").AddComponent<GameObjectsAsyncPoolsRoot>();
                }

                return _instance;
            }
        }

        private Dictionary<AssetReferenceT<GameObject>, GameObjectAsyncPool> pools =
            new Dictionary<AssetReferenceT<GameObject>, GameObjectAsyncPool>(16);


        public GameObjectAsyncPool GetPool(AssetReferenceT<GameObject> assetReference)
        {
            if (pools.ContainsKey(assetReference) == false)
            {
                var factory = new GameObjectAsyncFactory(assetReference, transform);
                var pool = new GameObjectAsyncPool(factory);

                pools.Add(assetReference, pool);
            }

            return pools[assetReference];
        }
    }
}