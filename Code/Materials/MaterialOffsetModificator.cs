using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TapNice.Scripts.GameCore.Utils.Materials
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