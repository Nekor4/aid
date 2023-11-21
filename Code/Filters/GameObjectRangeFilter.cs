using UnityEngine;

namespace Aid.Filters
{
    public class GameObjectRangeFilter : MonoBehaviour, IGameObjectFilter
    {
        [SerializeField] private float range;

        private float Range => transform.lossyScale.x * range;

        public bool IsPassing(GameObject detectable)
        {
            return Vector3.Distance(transform.position, detectable.transform.position) <= Range;
        }
        
        private void OnDrawGizmos()
        {
            var color = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
            Gizmos.color = color;
        }
    }
}