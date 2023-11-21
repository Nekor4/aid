using System.Collections.Generic;
using Aid.Filters;
using UnityEngine;

namespace Aid.Detector
{
    public class DetectablesRegistry : MonoBehaviour
    {
        private static DetectablesRegistry _instance;

        public static DetectablesRegistry Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("DetectablesRegistry").AddComponent<DetectablesRegistry>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        private readonly List<IDetectable> _detectables = new();

        public void Register(IDetectable detectable)
        {
            if (_detectables.Contains(detectable)) return;

            _detectables.Add(detectable);
        }

        public void UnRegister(IDetectable detectable)
        {
            _detectables.Remove(detectable);
        }

        public List<IDetectable> Detect(IReadOnlyList<IGameObjectFilter> filters)
        {
            var list = new List<IDetectable>();
            for (int i = 0; i < _detectables.Count; i++)
            {
                if (IsPassingFilters(_detectables[i], filters))
                    list.Add(_detectables[i]);
            }

            return list;
        }

        private bool IsPassingFilters(IDetectable detectable, IReadOnlyList<IGameObjectFilter> filters)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].IsPassing(detectable.transform.gameObject) == false) return false;
            }

            return true;
        }
    }
}