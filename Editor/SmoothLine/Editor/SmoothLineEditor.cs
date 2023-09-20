using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SmoothLine))]
public class SmoothLineEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SmoothLine myScript = (SmoothLine) target;
		if (GUILayout.Button("Smooth Line"))
		{
			myScript.Smooth();
		}
	}
}