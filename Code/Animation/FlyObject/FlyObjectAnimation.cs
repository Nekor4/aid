using System;
using Aid.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aid.Animation.FlyObject
{
    [CreateAssetMenu(menuName = "Aid/Animation/Fly Object Animation")]
    public class FlyObjectAnimation : ScriptableObject
    {
        public FlyObjectPoolSO pool;

        [SerializeField] private FlySetting settings;

        public async void Play(Vector3 startPosition, Vector3 targetPosition, Vector3 upAxis, Action completed)
        {
            var flyObject = pool.Request();
            settings.upAxis = upAxis;
            flyObject.Set(settings, startPosition, targetPosition, null);
            await UniTask.WaitForSeconds(settings.duration);
            completed?.Invoke();
            pool.Return(flyObject);
        }

        [Serializable]
        public struct FlySetting
        {
            public float duration;
            public AnimationCurve positionCurve, scaleCurve;

            public Vector3 upAxis;
            // [SerializeField] private Vector3 controlPointMinOffset, controlPointMaxOffset;
            //
            // public Vector3 RandomControlPointOffset =>
            //     RandomExtensions.Range(controlPointMinOffset, controlPointMaxOffset);
        }
    }
}