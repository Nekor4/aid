using UnityEngine;

namespace Aid.Extensions
{
    public static class AnimationCurveExtensions
    {
        public static int[] CurveToValues(this AnimationCurve curve, int count)
        {
            var tempValues = new int[count];

            for (int i = 0; i < count; i++)
            {
                var value = (int)curve.Evaluate(i);
                tempValues[i] = value;
            }

            return tempValues;
        }

        public static AnimationCurve ValuesToCurve(int[] values)
        {
            var curve = new AnimationCurve();

            for (int i = 0; i < values.Length; i++)
            {
                curve.AddKey(i, values[i]);
            }

            return curve;
        }
    }
}