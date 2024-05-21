using System;
using Aid.Extensions;
using UnityEngine;

namespace Aid.Animation.FlyObject
{
    [CreateAssetMenu(menuName = "Aid/Animation/Fly Object Animation")]
    public class FlyObjectAnimation : ScriptableObject
    {
        public FlyObjectPoolSO pool;

        [SerializeField] private FlySetting settings;

        public void Play(Vector3 startPosition, Vector3 targetPosition, Vector3 upAxis, Action completed)
        {
            var flyObject = pool.Request();
            flyObject.Set(settings, startPosition, targetPosition, completed);
        }

        [Serializable]
        public struct FlySetting
        {
            public float duration;
            public AnimationCurve positionCurve, scaleCurve;
            [SerializeField] private Vector3 controlPointMinOffset, controlPointMaxOffset;

            public Vector3 RandomControlPointOffset =>
                RandomExtensions.Range(controlPointMinOffset, controlPointMaxOffset);
        }
    }
}