using System;
using UnityEngine;

namespace Aid.Detector
{
    public class DetectableObject : MonoBehaviour, IDetectable
    {
        public bool IsDetectable => gameObject.activeSelf;
        public event Action<IDetectable> DetectionChanged;
        private void OnEnable()
        {
            DetectablesRegistry.Instance.Register(this);
            DetectionChanged?.Invoke(this);
        }

        private void OnDisable()
        {
            DetectablesRegistry.Instance.UnRegister(this);
            DetectionChanged?.Invoke(this);
        }
    }
}