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

        public List<IDetectable> Detectables => _detectables;

        public void Register(IDetectable detectable)
        {
            if (_detectables.Contains(detectable)) return;

            _detectables.Add(detectable);
        }

        public static void UnRegister(IDetectable detectable)
        {
            if (_instance == null) return;
            
            _instance._detectables.Remove(detectable);
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
#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DetectablesRegistry))]
    public class DetectablesRegistryEditor : UnityEditor.Editor
    {
        private bool _showDetectedList;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UnityEditor.EditorGUILayout.Separator();
            var detector = (DetectablesRegistry)target;


            var detected = detector.Detectables;

            if (detected.Count <= 0)
            {
                GUILayout.Label($"Nothing detected");
            }
            else
            {
                _showDetectedList = UnityEditor.EditorGUILayout.Foldout(_showDetectedList, $"Detected ({detected.Count}): ");
                if (_showDetectedList)
                {
                    UnityEditor.EditorGUI.BeginDisabledGroup(true);
                    for (int i = 0; i < detected.Count; i++)
                    {
                        UnityEditor.EditorGUILayout.ObjectField(detected[i].transform, typeof(Transform), true);
                    }

                    UnityEditor.EditorGUI.EndDisabledGroup();
                }
            }
        }
    }
#endif
}