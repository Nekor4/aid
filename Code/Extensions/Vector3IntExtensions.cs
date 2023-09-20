using UnityEngine;

namespace Aid
{
	public static class Vector3IntExtensions
	{
		public static Vector3 ToVector3(this Vector3Int vector3Int)
		{
			return new Vector3(vector3Int.x, vector3Int.y, vector3Int.z);
		}

		public static Vector2 ToVector2(this Vector3Int vector3Int)
		{
			return new Vector2(vector3Int.x, vector3Int.y);
		}
	}
}