#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameCore.Utils
{
	public class MaterialFinder : MonoBehaviour
	{
		[SerializeField] private List<ShaderParams> shaderParams = new List<ShaderParams>();
		[SerializeField] private List<ShaderParams> standardShaderParams = new List<ShaderParams>();
		[SerializeField] private Material replaceStandardMaterial;

		public void Clear()
		{
			shaderParams.Clear();
			standardShaderParams.Clear();
		}

		public void CollectMaterials(GameObject go)
		{
			var mr = go.GetComponent<MeshRenderer>();
			if (mr != null)
			{
				if (mr.sharedMaterial != null)
				{
					for (int iMat = 0; iMat < mr.sharedMaterials.Length; iMat += 1)
					{
						AddMat(mr.sharedMaterials[iMat], go);
					}
				}
			}

			for (int i = 0; i < go.transform.childCount; i += 1)
			{
				CollectMaterials(go.transform.GetChild(i).gameObject);
			}
		}

		public void ReplaceStandards()
		{
			if (!replaceStandardMaterial)
			{
				Debug.LogError("We don't have material for replace!");
				return;
			}

			foreach (var s in standardShaderParams)
			{
				foreach (var m in s.materials)
				{
					foreach (var o in m.objects)
					{
						var mr = o.GetComponent<MeshRenderer>();
						if (mr)
						{
							mr.sharedMaterial = replaceStandardMaterial;
						}
					}
				}
			}
		}

		void AddMat(Material material, GameObject go)
		{
			if (material == null)
				return;

			ShaderParams sp = GetShaderParam(shaderParams, material.shader.name);
			if (sp == null)
			{
				sp = new ShaderParams();
				sp.shaderName = material.shader.name;
				sp.materials = new List<GameObjectMat>();

				shaderParams.Add(sp);

				if (sp.shaderName.ToLower().Contains("standard"))
				{
					standardShaderParams.Add(sp);
				}
			}

			sp.AddMaterial(material, go);
		}

		private ShaderParams GetShaderParam(List<ShaderParams> sp, string name)
		{
			foreach (var p in sp)
				if (p.shaderName.Equals(name))
					return p;

			return null;
		}
	}

	[Serializable]
	public class ShaderParams
	{
		[HideInInspector] public string displayName;
		public string shaderName;
		[HideInInspector] public int objectsCount;
		public List<GameObjectMat> materials;

		public void AddMaterial(Material m, GameObject go)
		{
			foreach (GameObjectMat t in materials)
			{
				if (t.mat == m)
				{
					if (!t.objects.Contains(go))
					{
						t.objects.Add(go);
						IncreaseObjectCounter();
					}

					return;
				}
			}

			var gom = new GameObjectMat();
			gom.matName = m.name;
			gom.mat = m;
			gom.objects = new List<GameObject>() {go};
			materials.Add(gom);
			IncreaseObjectCounter();
		}

		private void IncreaseObjectCounter()
		{
			objectsCount++;
			displayName = string.Format("[{0}] {1}", objectsCount.ToString(), shaderName);
		}
	}

	[Serializable]
	public struct GameObjectMat
	{
		[HideInInspector] public string matName;
		public Material mat;
		public List<GameObject> objects;
	}

	[CustomEditor(typeof(MaterialFinder))]
	[CanEditMultipleObjects()]
	public class MaterialFinderEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Collect"))
			{
				foreach (var t in targets)
				{
					MaterialFinder myTarget = (MaterialFinder) t;
					myTarget.Clear();
					myTarget.CollectMaterials(myTarget.gameObject);
				}
			}

//		if (GUILayout.Button ("Destroy StandardShader Materials")) {
//			foreach (var t in targets) {
//				MaterialFinder myTarget = (MaterialFinder)t;
//
//				bool foundAny = false;
//
//				foreach (var m in myTarget.foundMats) {
//					if (m.Value.isStandard) {
//						foundAny = true;
//						var path = AssetDatabase.GetAssetPath (m.Key);
//						var delete = EditorUtility.DisplayDialog ("Remove material (no undo)", path, "delete", "cancel");
//						if (delete) {
//							DestroyImmediate (m.Key, true);
//							AssetDatabase.DeleteAsset (path);
//						}
//					}
//				}
//
//				if (!foundAny) {
//					EditorUtility.DisplayDialog ("No standard shader materials found", "", "Ok");
//				} else {
//					AssetDatabase.Refresh ();
//				}
//			}
//		}

			base.OnInspectorGUI();

			if (GUILayout.Button("Replace 'Standards'"))
			{
				foreach (var t in targets)
				{
					MaterialFinder myTarget = (MaterialFinder) t;
					myTarget.ReplaceStandards();
				}
			}
		}
	}
}
#endif