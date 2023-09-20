using UnityEngine;

namespace Aid.Factory
{[CreateAssetMenu( menuName = "Aid/Factory/Prefab Factory")]

    public class PrefabFactory : FactorySO<GameObject>
    {
        [SerializeField] protected GameObject prefab;
        public override GameObject Create()
        {
            return Instantiate(prefab);
        }
    }
}