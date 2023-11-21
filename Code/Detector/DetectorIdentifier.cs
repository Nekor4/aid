using UnityEngine;

namespace Aid.Detector
{
    public class DetectorIdentifier : MonoBehaviour
    {
        [SerializeField]
        private string id;

        public string ID => id;
        
        public IDetector Detector { get; private set; }

        private void Awake()
        {
            Detector = GetComponent<IDetector>();
        }
    }
}