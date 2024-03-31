using Aid.Factory;
using Aid.Factory.Serializable;
using UnityEngine;

namespace Aid.Pool.Components
{
    [CreateAssetMenu(menuName = "Aid/Pool/Trail Renderer Pool")]
    public class TrailRendererPoolSO : ComponentPoolSO<TrailRenderer>
    {
        [SerializeField] private SerializableComponentFactory<TrailRenderer> factory;
        public override IFactory<TrailRenderer> Factory => factory;
    }
}