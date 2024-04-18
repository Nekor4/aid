using System.Collections.Generic;
using Aid.Filters;
using Aid.Singletons;
using UnityEngine;

namespace Aid.Detector
{
    public class DetectablesRegistry : PersistentMonoSingleton<DetectablesRegistry>
    {
        public List<IDetectable> Detectables => _detectables;

        private readonly List<IDetectable> _detectables = new();

        public void Register(IDetectable detectable)
        {
            if (_detectables.Contains(detectable)) return;

            _detectables.Add(detectable);
        }

        public static void UnRegister(IDetectable detectable)
        {
            if (InstanceExists)
                Instance._detectables.Remove(detectable);
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

        public List<IDetectable> GetInRange(Vector3 position, float range)
        {
            List<IDetectable> list = new();
            for (int i = 0; i < _detectables.Count; i++)
            {
                if (!IsInRange(position, range, _detectables[i]))
                    continue;

                list.Add(_detectables[i]);
            }

            return list;
        }

        public List<IDetectable> OverlapBox(Vector3 position, Vector3 size, Quaternion rotation)
        {
            List<IDetectable> list = new();

            foreach (IDetectable detectable in _detectables)
            {
                Vector3 objectPosition = detectable.Owner.transform.position;

                Vector3 halfSize = size / 2;

                Vector3 localPosition = Quaternion.Inverse(rotation) * (objectPosition - position);

                if (Mathf.Abs(localPosition.x) < halfSize.x && Mathf.Abs(localPosition.y) < halfSize.y)
                {
                    list.Add(detectable);
                }
            }

            return list;
        }

        private bool IsInRange(Vector3 position, float range, IDetectable detectable)
        {
            return Vector3.Distance(position, detectable.Owner.transform.position) <= range;
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