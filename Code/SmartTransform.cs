using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Aid
{
	public class SmartTransform : MonoBehaviour
	{
		public bool RevertOnAwake;

		[SerializeField] [HideInInspector] private TransformUtils.TransformCopy transformCopy;

		public void SaveTransform()
		{
			transformCopy = TransformUtils.Copy(transform);
		}

		public void Revert()
		{
			TransformUtils.CopyTo(transformCopy, transform);
		}

		void Awake()
		{
			if (RevertOnAwake)
			{
				Revert();
			}
		}
	}


#if UNITY_EDITOR
	[CustomEditor(typeof(SmartTransform))]
	[CanEditMultipleObjects()]
	public class SmartTransformEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Save"))
			{
				foreach (var t in targets)
				{
					SmartTransform myTarget = (SmartTransform) t;
					myTarget.SaveTransform();
				}
			}

			base.OnInspectorGUI();
		}
	}
#endif
}