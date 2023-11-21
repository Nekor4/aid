using Aid.Factory.Async;
using UnityEngine;

namespace Aid.Pool.Async
{
    public class GameObjectAsyncPool : AsyncPool<GameObject>
    {
        public GameObjectAsyncPool(IAsyncFactory<GameObject> factory) : base(factory)
        {
        }
    }
}