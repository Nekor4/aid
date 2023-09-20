using Aid.Factory;
using Aid.Pool.Async;
using UnityEngine;

namespace Aid.Pool
{
    public class GameObjectAsyncPool : AsyncPool<GameObject>
    {
        public GameObjectAsyncPool(IAsyncFactory<GameObject> factory) : base(factory)
        {
        }
    }
}