using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aid.UI
{
	// [Serializable, CreateAssetMenu]
	public class UIConfig : ScriptableObject
	{
		public float planeDistance = 1f;
		public CanvasScaler.ScaleMode uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		public Vector2 referenceResolution = new Vector2(1080, 1920);
		public CanvasScaler.ScreenMatchMode screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		[Range(0f, 1f)] public float matchWidthOrHeight = .5f;
	}
}