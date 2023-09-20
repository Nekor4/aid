using UnityEngine;
using Random = UnityEngine.Random;

namespace TapNice.Scripts.GameCore.Utils.Materials
{
    public class MaterialColorModificator : MaterialModificator
    {
        [SerializeField] private string colorPropertyName;
        [SerializeField] private Gradient gradient;
        [SerializeField] private float minDuration = 2, maxDuration = 5;

        private float time, currentDuration;

        private bool invert;

        private int colorID;

        private bool hasCustomProperty;

        private void Start()
        {
            if (string.IsNullOrEmpty(colorPropertyName) == false)
            {
                colorID = Shader.PropertyToID(colorPropertyName);
                hasCustomProperty = true;
            }

            currentDuration = Random.Range(minDuration, maxDuration);
        }

        private void OnValidate()
        {
            currentDuration = Random.Range(minDuration, maxDuration);
        }

        public override void UpdateMaterial()
        {
            if (invert)
                time -= Time.deltaTime;
            else
                time += Time.deltaTime;

            if (time >= currentDuration)
            {
                invert = true;
            }
            else if (time <= 0)
            {
                currentDuration = Random.Range(minDuration, maxDuration);
                time = 0;
                invert = false;
            }

            var t = time / currentDuration;

            if (hasCustomProperty)
                material.SetColor(colorID, gradient.Evaluate(t));
            else
                material.color = gradient.Evaluate(t);
        }
    }
}