using System.Collections.Generic;
using UnityEngine;

namespace Aid.Materials
{
    public class MaterialModificatorProcessor : MonoBehaviour
    {
        private Dictionary<Material, List<MaterialModificator>> modifictaors =
            new Dictionary<Material, List<MaterialModificator>>();

        private static MaterialModificatorProcessor instance;

        public static MaterialModificatorProcessor Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameObject("Materials Processor", typeof(MaterialModificatorProcessor))
                        .GetComponent<MaterialModificatorProcessor>();
                return instance;
            }
        }

        public static bool InstanceExist => instance != null;

        public void Register(MaterialModificator modificator)
        {
            if (modificator.UseIndependentMaterial)
            {
                modificator.Material = Instantiate(modificator.Renderer.sharedMaterial);
                modifictaors.Add(modificator.Material, new List<MaterialModificator> { modificator });
            }
            else
            {
                if (modifictaors.ContainsKey(modificator.Renderer.sharedMaterial))
                {
                    var list = modifictaors[modificator.Renderer.sharedMaterial];
                    modificator.Material = list[0].Material;
                    list.Add(modificator);
                }
                else
                {
                    modifictaors.Add(modificator.Renderer.sharedMaterial,
                        new List<MaterialModificator> { modificator });
                    modificator.Material = Instantiate(modificator.Renderer.sharedMaterial);
                }
            }
        }

        public void UnRegister(MaterialModificator modificator)
        {
            var list = modifictaors[modificator.OrginalMaterial];
            list.Remove(modificator);
            if (list.Count <= 0)
            {
                modifictaors.Remove(modificator.OrginalMaterial);
                Destroy(modificator.Material);
            }
        }

        private void Update()
        {
            foreach (var modificator in modifictaors)
            {
                modificator.Value[0].UpdateMaterial();
            }
        }
    }
}