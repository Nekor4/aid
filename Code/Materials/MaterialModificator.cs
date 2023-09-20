using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TapNice.Scripts.GameCore.Utils.Materials
{
    public abstract class MaterialModificator : MonoBehaviour
    {
        [SerializeField] [FormerlySerializedAs("mat")]
        protected Material material;

        public bool useIndependentMaterial;
        private readonly List<Renderer> renderers = new List<Renderer>(12);

        private bool isRegistered;
        public bool UseIndependentMaterial => useIndependentMaterial;

        public Material Material
        {
            get => material;
            set
            {
                material = value;
                for (int i = 0; i < renderers.Count; i++)
                {
                    renderers[i].sharedMaterial = value;
                }
            }
        }

        public Material OrginalMaterial { get; private set; }
        
        public Renderer Renderer => renderers[0];

        private void Awake()
        {
            OrginalMaterial = material;
            FindRenderers();
            MaterialModificatorProcessor.Instance.Register(this);
            isRegistered = true;
        }

        private void OnDestroy()
        {
            if (isRegistered && MaterialModificatorProcessor.InstanceExist)
                MaterialModificatorProcessor.Instance.UnRegister(this);
        }

        private void FindRenderers()
        {
            var allRenderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < allRenderers.Length; i++)
            {
                if (allRenderers[i].sharedMaterial == material)
                    renderers.Add(allRenderers[i]);
            }
        }

        public abstract void UpdateMaterial();
    }
}