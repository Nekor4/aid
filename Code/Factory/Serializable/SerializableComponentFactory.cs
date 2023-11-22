using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aid.Factory.Serializable
{
    [Serializable]
    public abstract class SerializableComponentFactory<T> : IFactory<T> where T : Component
    {
        [SerializeField] private T prefab;
        public T Create()
        {
            return Object.Instantiate(prefab);
        }
    }
}