using UnityEngine;

namespace Aid
{
	public static class InputExtensions
	{
		public static Vector2 GetMousePositionRelativeToScreen()
		{
			Vector2 ret = Input.mousePosition;
			ret.x /= Screen.width;
			ret.y /= Screen.height;
			return ret;
		}
	}
}