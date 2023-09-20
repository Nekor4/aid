using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Utils
{
    public class MaterialGroup
    {
        private Dictionary<Material, MaterialData> materialsData;

        public MaterialGroup(Renderer[] renderers)
        {
            materialsData = new Dictionary<Material, MaterialData>();
            
            for (int i = 0; i < renderers.Length; i += 1)
            {
                if (renderers[i].sharedMaterial != null)
                {
                    if (!materialsData.ContainsKey(renderers[i].sharedMaterial))
                    {
                        materialsData.Add(renderers[i].sharedMaterial, new MaterialData(renderers[i].sharedMaterial));
                    }

                    materialsData[renderers[i].sharedMaterial].AddRenderer(renderers[i]);
                }
            }
        }

        public void SetProperty(string name, float value)
        {
            foreach (var materialData in materialsData)
            {
                materialData.Value.SetProperty(name, value);
            }
        }

        public void SetProperty(string name, Color value)
        {
            foreach (var materialData in materialsData)
            {
                materialData.Value.SetProperty(name, value);
            }
        }

        public void RestoreRenderers()
        {
            foreach (var materialData in materialsData)
            {
                materialData.Value.UseOrigin();
            }
        }

        private class MaterialData
        {
            public MaterialData(Material originMat)
            {
                this.originMat = originMat;
            }

            private readonly List<Renderer> renderers = new List<Renderer>();
            private readonly Material originMat;
            private Material instancedMat;
            private bool haveInstance;
            private bool isUsingInstance;

            public void AddRenderer(Renderer renderer)
            {
                renderers.Add(renderer);
            }

            public void UseOrigin()
            {
                if (!isUsingInstance) return;
                
                isUsingInstance = false;
                for (int i = 0; i < renderers.Count; i += 1)
                {
                    if(renderers[i] != null)
                        renderers[i].sharedMaterial = originMat;
                }
            }

            public void SetProperty(string name, float value)
            {
                if (originMat.HasProperty(name))
                {
                    UseInstanced();
                    instancedMat.SetFloat(name, value);
                }
            }

            public void SetProperty(string name, Color value)
            {
                if (originMat.HasProperty(name))
                {
                    UseInstanced();
                    instancedMat.SetColor(name, value);
                }
            }

            public void UseInstanced()
            {
                if (!haveInstance)
                {
                    instancedMat = Material.Instantiate(originMat);
                    haveInstance = true;
                }

                if (isUsingInstance) return;
                
                isUsingInstance = true;
                for (int i = 0; i < renderers.Count; i += 1)
                {
                    if(renderers[i] != null)
                        renderers[i].sharedMaterial = instancedMat;
                }
            }
        }
    }
}