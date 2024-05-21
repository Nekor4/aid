using Aid.Factory;
using Aid.Factory.Serializable;
using UnityEngine;

namespace Aid.Pool.Components
{
    [CreateAssetMenu(menuName = "Aid/Pool/Particle System Pool")]
    public class ParticleSystemPoolSO : ComponentPoolSO<ParticleSystemPoolItem>
    {
        [SerializeField] private SerializableComponentFactory<ParticleSystemPoolItem> factory;
        public override IFactory<ParticleSystemPoolItem> Factory => factory;

        protected override ParticleSystemPoolItem Create()
        {
            var item = base.Create();
            item.SetOwnerPool(this);
            return item;
        }
    }
}