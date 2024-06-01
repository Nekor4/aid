using System;
using UnityEngine;

namespace Aid.Detector
{
    public interface IDetectable
    {
        public Vector3 Position { get; }
        public float Radius { get; }
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public bool IsDetectable { get; }
        public event Action<IDetectable> DetectionChanged;
    }
}