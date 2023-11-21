using UnityEngine;

namespace Aid.Extensions
{
	public static class MathExtension
	{
		public static float GetPercent(float complete, float total)
		{
			return (100 * complete) / total;
		}

		public static float GetPercent01Unclamped(float complete, float total)
		{
			return complete / total;
		}

		public static float GetPercent01Clameped(float complete, float total)
		{
			return Mathf.Clamp01(complete / total);
		}

		public static Vector3 CubicBezier(Vector3 start, Vector3 control, Vector3 end, float t)
		{
			return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * control + t * t * end;
		}
		
		public static float DistanceXZ(Vector3 from, Vector3 to)
		{
			var v1 = new Vector2(from.x, from.z);
			var v2 = new Vector2(to.x, to.z);
			return Vector2.Distance(v1, v2);
		}

		public static float SmoothStepAngle(float from, float to, float t)
		{
			to = from + Mathf.DeltaAngle(from, to);
			return Mathf.SmoothStep(from, to, t);
		}

		public static Vector3 CenterOfVectors( params Vector3[] vectors )
		{
			Vector3 sum = Vector3.zero;
			if( vectors == null || vectors.Length == 0 )
			{
				return sum;
			}
     
			foreach( Vector3 vec in vectors )
			{
				sum += vec;
			}
			return sum/vectors.Length;
		}
		
		public static Vector3 PointOnCircle(Vector3 center, float radius, float percent)
		{
			var ang = percent * 360;
			Vector3 pos;
			pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
			pos.y = center.y;
			return pos;
		}

		public static Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
		{
			return PointOnCircle(center, radius, UnityEngine.Random.Range(.025f, 1));
		}
	}
}