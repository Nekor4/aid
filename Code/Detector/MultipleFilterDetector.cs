using UnityEngine;

namespace Aid.Detector
{
    public class MultipleFilterDetector : FilterDetector
    {
        [SerializeField]
        private MultipleDetectorFilterHolder[] filterHolders;

        private void OnValidate()
        {
            filterHolders = GetComponentsInChildren<MultipleDetectorFilterHolder>();
        }
        protected override void Refresh()
        {
            for (int i = 0; i < filterHolders.Length; i++)
            {
                UpdateDetected(filterHolders[i].Filters);
            }

            UpdateClosest();
        }
    }
}