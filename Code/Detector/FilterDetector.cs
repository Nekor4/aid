using System;
using System.Collections.Generic;
using Aid.Filters;
using Aid.UpdateManager;
using UnityEngine;

namespace Aid.Detector
{
    public class FilterDetector : MonoBehaviour, IDetector
    {
        [SerializeField] private int refreshFrameInterval = 10;
        public bool IsAnyDetected => AllDetected.Count > 0 && GetClosest() != null;
        public IReadOnlyList<IDetectable> AllDetected => _detected;
        public event Action<IDetectable> ObjectDetected;
        public event Action<IDetectable> ObjectLostDetection;

        private readonly List<IDetectable> _detected = new List<IDetectable>(32);
        private IUpdateEntity _updateEntity;

        private IDetectable _closest;

        private readonly List<IGameObjectFilter> _filters = new(2);

        private void Awake()
        {
            GetComponentsInChildren(_filters);
        }

        public IDetectable GetClosest()
        {
            if ((_closest as UnityEngine.Object) == null)
                _closest = null;
            return _closest;
        }

        public void Clear()
        {
            for (int i = 0; i < _detected.Count; i++)
            {
                ObjectLostDetection?.Invoke(_detected[i]);
            }

            _detected.Clear();
        }

        private void OnEnable()
        {
            _updateEntity = AdvanceUpdateManager.StartUpdate(Refresh, UpdateType.FixedUpdate, refreshFrameInterval);
        }

        private void OnDisable()
        {
            AdvanceUpdateManager.Stop(_updateEntity);
        }

        protected virtual void Refresh()
        {
            UpdateDetected(_filters);
            UpdateClosest();
        }

        protected void UpdateDetected(IReadOnlyList<IGameObjectFilter> filters)
        {
            var newDetected = DetectablesRegistry.Instance.Detect(filters);

            for (int i = 0; i < _detected.Count; i++)
            {
                if (newDetected.Contains(_detected[i])) continue;
                ObjectLostDetection?.Invoke(_detected[i]);
                _detected.RemoveAt(i);
                i--;
            }

            for (int i = 0; i < newDetected.Count; i++)
            {
                if (_detected.Contains(newDetected[i])) continue;
                _detected.Add(newDetected[i]);
                ObjectDetected?.Invoke(newDetected[i]);
            }
        }

        private protected void UpdateClosest()
        {
            if (AllDetected.Count == 0)
            {
                _closest = null;
                return;
            }

            var closest = AllDetected[0];
            for (int i = 0; i < AllDetected.Count; i++)
            {
                if (Vector3.Distance(transform.position, AllDetected[i].transform.position) <
                    Vector3.Distance(transform.position, closest.transform.position))
                {
                    closest = AllDetected[i];
                }
            }

            _closest = closest;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(FilterDetector))]
    public class RangeDetectorEditor : UnityEditor.Editor
    {
        private bool _showDetectedList;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UnityEditor.EditorGUILayout.Separator();
            var detector = (IDetector)target;


            var detected = detector.AllDetected;

            if (detected.Count <= 0)
            {
                GUILayout.Label($"Nothing detected");
            }
            else
            {
                _showDetectedList =
                    UnityEditor.EditorGUILayout.Foldout(_showDetectedList, $"Detected ({detected.Count}): ");
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