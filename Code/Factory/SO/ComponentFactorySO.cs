using UnityEngine;

namespace Aid.Factory.SO
{
    public abstract class ComponentFactorySO<T> : FactorySO<T> where T : Component
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