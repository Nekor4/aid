using Aid.Factory;
using Aid.Factory.Serializable;
using UnityEngine;

namespace Aid.Pool
{
    [CreateAssetMenu(menuName = "Aid/Pool/Prefab Pool")]
    public class PrefabPool : PoolSO<GameObject>
    {
        [SerializeField] private SerializablePrefabFactory factory;

        public override IFactory<GameObject> Factory
        {
            get => factory;
        }

        public override GameObject Request()
        {
            var item = base.Request();
            item.SetActive(true);
            return item;
        }

        public override void Return(GameObject member)
        {
            member.SetActive(false);
            base.Return(member);
        }
    }
}