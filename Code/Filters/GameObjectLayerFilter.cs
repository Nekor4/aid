using UnityEngine;

namespace Aid.Filters
{
    public class GameObjectLayerFilter : MonoBehaviour, IGameObjectFilter
    {
        [SerializeField] private LayerMask mask;
        public bool IsPassing(GameObject detectable)
        {
            return IsInLayer(detectable, mask);
        }
        private bool IsInLayer(GameObject go, LayerMask mask)
        {
            return (mask & (1 << go.layer)) != 0;
        }
    }
}