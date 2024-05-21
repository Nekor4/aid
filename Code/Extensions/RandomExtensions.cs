using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aid.Extensions
{
    public static class RandomExtensions
    {
        private static System.Random rnd = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue));

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        public static void ShuffleThis<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        public static bool RandomFlag() => UnityEngine.Random.value > 0.5f;

        public static Vector3 Range(Vector3 v1, Vector3 v2)
        {
            var x = UnityEngine.Random.Range(v1.x, v2.x);
            var y = UnityEngine.Random.Range(v1.y, v2.y);
            var z = UnityEngine.Random.Range(v1.z, v2.z);
            return new Vector3(x, y, z);
        }

        public static float Gauss(float mean, float standardDeviation)
        {
            var u1 = 1.0f - UnityEngine.Random.value;
            var u2 = 1.0f - UnityEngine.Random.value;

            var randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

            return mean + standardDeviation * randStdNormal;
        }
    }
}