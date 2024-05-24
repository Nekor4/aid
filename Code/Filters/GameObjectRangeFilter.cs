using UnityEngine;

namespace Aid.Filters
{
    public class GameObjectRangeFilter : MonoBehaviour, IGameObjectFilter
    {
        [SerializeField] private float range;

        private float Range => transform.lossyScale.x * range;

        public void ChangeRange(float newRange)
        {
            range = newRange;
        }

        public bool IsPassing(GameObject detectable)
        {
            if (range <= 0)
                return true;

            return Vector3.Distance(transform.position, detectable.transform.position) <= Range;
        }

        private void OnDrawGizmos()
        {
            if (enabled == false) return;
            var color = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
            Gizmos.color = color;
        }
    }
}