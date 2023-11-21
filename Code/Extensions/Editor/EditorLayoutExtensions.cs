using UnityEngine;

namespace Aid.Extensions.Editor
{
	public static class EditorLayoutExtensions
	{
		public static void HorizontalSeparator()
		{
			GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(1));
		}

		public static void VerticalSeparator()
		{
			GUILayout.Box(string.Empty, GUILayout.ExpandHeight(true), GUILayout.Width(1));
		}
	}
}