using System;
using UnityEngine;

namespace Aid.Detector
{
    public class DetectableObject : MonoBehaviour, IDetectable
    {
        public GameObject Owner { get; set; }
        public bool IsDetectable => gameObject.activeSelf;
        public event Action<IDetectable> DetectionChanged;
        private void OnEnable()
        {
            DetectablesRegistry.Instance.Register(this);
            DetectionChanged?.Invoke(this);
        }

        private void OnDisable()
        {
            DetectablesRegistry.UnRegister(this);
            DetectionChanged?.Invoke(this);
        }
    }
}