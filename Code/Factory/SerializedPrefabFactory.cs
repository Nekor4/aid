using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aid.Factory
{
    [Serializable]
    public class SerializedPrefabFactory : IFactory<GameObject>
    {
        [SerializeField] private GameObject prefab;
        public GameObject Create()
        {
            return Object.Instantiate(prefab);
        }
    }
}