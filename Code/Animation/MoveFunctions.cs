using System;
using Aid.Extensions;
using Aid.Transitions;
using UnityEngine;

namespace Aid.Animation
{
    public static class MoveFunctions
    {
        public static void CubicMove(this Transform transform, float duration, Func<Transform, Vector3> startGetter,
            Func<Vector3> controlGetter, Func<Vector3> endGetter)
        {
            TransitionsManager.StartTransition(duration, progress =>
            {
                transform.position = MathExtension.CubicBezier(startGetter.Invoke(transform), controlGetter.Invoke(),
                    endGetter.Invoke(), progress);
            }, null);
        }
    }
}