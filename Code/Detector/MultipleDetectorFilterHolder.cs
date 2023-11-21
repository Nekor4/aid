using System.Collections.Generic;
using Aid.Filters;
using UnityEngine;

namespace Aid.Detector
{
    public class MultipleDetectorFilterHolder : MonoBehaviour
    {
        private readonly List<IGameObjectFilter> _filters = new();
        public IReadOnlyList<IGameObjectFilter> Filters => _filters;

        private void Awake()
        {
            GetComponentsInChildren(_filters);
        }
    }
}