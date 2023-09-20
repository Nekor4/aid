using UnityEngine;

namespace Aid.Factory
{
    public abstract class ComponentFactory<T> : FactorySO<T> where T : Component
    {
        [SerializeField] private T prefab;
        
        public T Prefab
        {
            set => prefab = value;
        }

        public override T Create()
        {
            return Instantiate(prefab);
        }
    }
}