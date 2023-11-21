using System;
using UnityEngine;

namespace Aid.Detector
{
    public interface IDetectable 
    {
        public Transform transform { get; }
        
        public bool IsDetectable { get; }
        public event Action<IDetectable> DetectionChanged;
        public bool CompareTag(string tag);
    }
}