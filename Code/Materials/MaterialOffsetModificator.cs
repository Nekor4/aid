using UnityEngine;

namespace Aid.Materials
{
    public class MaterialOffsetModificator : MaterialModificator
    {
        public Vector2 speed;
        public override void UpdateMaterial()
        {
            var predictionMatOffset = material.mainTextureOffset;
            predictionMatOffset += Time.deltaTime * speed;
            predictionMatOffset.x %= 1;
            predictionMatOffset.y %= 1;
            material.mainTextureOffset = predictionMatOffset;
        }
    }
}