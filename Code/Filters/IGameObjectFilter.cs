using UnityEngine;

namespace Aid.Filters
{
    public interface IGameObjectFilter
    {
        bool IsPassing(GameObject detectable);
    }
}