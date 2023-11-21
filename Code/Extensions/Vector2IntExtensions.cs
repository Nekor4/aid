using UnityEngine;

namespace Aid.Extensions
{
	public static class Vector2IntExtensions
	{
		public static Vector3 ToVector3(this Vector2Int vector2Int)
		{
			return new Vector3(vector2Int.x, vector2Int.y);
		}

		public static Vector2 ToVector2(this Vector2Int vector2Int)
		{
			return new Vector2(vector2Int.x, vector2Int.y);
		}
	}
}