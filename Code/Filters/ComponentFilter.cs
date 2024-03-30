using UnityEngine;

namespace Aid.Filters
{
    public abstract class ComponentFilter<T> : AbstractScriptableObjectFilter, IGameObjectFilter where T : Component
    {
        public override bool IsPassing(GameObject detectable)
        {
            return detectable.TryGetComponent<T>(out _);
        }
    }
}