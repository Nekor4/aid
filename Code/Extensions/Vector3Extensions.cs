using UnityEngine;

namespace Aid.Extensions
{
	public static class Vector3Extensions
	{
		public static float DistanceToLine(this Vector3 point, Ray ray)
		{
			return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
		}

		public static Vector2Int ToVector2Int(this Vector3 vector3)
		{
			return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
		}

		public static Vector3Int ToVector3Int(this Vector3 vector3)
		{
			return new Vector3Int((int) vector3.x, (int) vector3.y, (int) vector3.z);
		}

		public static Vector3Int ToClosestVector3Int(this Vector3 vector3)
		{
			return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
		}

		public static Vector3Int CeilToVector3Int(this Vector3 vector3)
		{
			return new Vector3Int(Mathf.CeilToInt(vector3.x), Mathf.CeilToInt(vector3.y), Mathf.CeilToInt(vector3.z));
		}

		public static Vector3 SmoothStep(Vector3 from, Vector3 to, float t)
		{
			return new Vector3(Mathf.SmoothStep(from.x, to.x, t),
				Mathf.SmoothStep(from.y, to.y, t),
				Mathf.SmoothStep(from.z, to.z, t));
		}

		public static Vector3 SmoothStepAngle(Vector3 from, Vector3 to, float t)
		{
			return new Vector3(MathExtension.SmoothStepAngle(from.x, to.x, t),
				MathExtension.SmoothStepAngle(from.y, to.y, t),
				MathExtension.SmoothStepAngle(from.z, to.z, t));
		}
	}
}