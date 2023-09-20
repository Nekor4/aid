using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Aid
{
	[ExecuteInEditMode]
	public class GameObjectId : MonoBehaviour
	{
		[SerializeField] private string id;

		public string Id
		{
			get { return id; }
		}

		private void Awake()
		{
			if (string.IsNullOrEmpty(id))
				id = Guid.NewGuid().ToString();
		}

		private void Reset()
		{
			id = Guid.NewGuid().ToString();
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(GameObjectId))]
	public class GameObjectIdEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var script = (GameObjectId) target;

			EditorGUI.BeginDisabledGroup(true);

			EditorGUILayout.TextField("ID:", script.Id);

			EditorGUI.EndDisabledGroup();
		}
	}
#endif
}