using UnityEngine;

namespace Aid.Filters
{
    public abstract class AbstractScriptableObjectFilter : ScriptableObject, IGameObjectFilter
    {
        public abstract bool IsPassing(GameObject detectable);
    }
}