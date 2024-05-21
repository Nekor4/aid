using Aid.Factory;
using Aid.Factory.Serializable;
using Aid.Pool;
using UnityEngine;

namespace Aid.Animation.FlyObject
{
    [CreateAssetMenu(menuName = "Aid/Animation/Fly Animator Pool")]
    public class FlyObjectPoolSO : ComponentPoolSO<FlyObjectAnimator>
    {
        [SerializeField] private SerializableComponentFactory<FlyObjectAnimator> factory;
        public override IFactory<FlyObjectAnimator> Factory => factory;
    }
}