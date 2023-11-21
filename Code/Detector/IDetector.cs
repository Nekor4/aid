using System.Collections.Generic;
using UnityEngine;

namespace Aid.Detector
{
    public interface IDetector 
    {
        GameObject gameObject { get; }
        bool IsAnyDetected { get; }
        public IReadOnlyList<IDetectable> AllDetected { get; }
        IDetectable GetClosest();
        void Clear();
    }
}