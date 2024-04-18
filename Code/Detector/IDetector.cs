using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aid.Detector
{
    public interface IDetector
    {
        public event Action<IDetectable> ObjectDetected;
        public event Action<IDetectable> ObjectLostDetection;

        public GameObject Owner { get; }
        public GameObject gameObject { get; }
        bool IsAnyDetected { get; }
        public IReadOnlyList<IDetectable> AllDetected { get; }
        IDetectable GetClosest();
        void Clear();
    }
}