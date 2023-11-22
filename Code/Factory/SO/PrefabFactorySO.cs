using UnityEngine;

namespace Aid.Factory.SO
{[CreateAssetMenu( menuName = "Aid/Factory/Prefab Factory")]

    public class PrefabFactorySO : FactorySO<GameObject>
    {
        [SerializeField] protected GameObject prefab;
        public override GameObject Create()
        {
            return Instantiate(prefab);
        }
    }
}