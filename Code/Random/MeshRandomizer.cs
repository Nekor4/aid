using UnityEngine;

namespace Aid.Random
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class MeshRandomizer : MonoBehaviour
	{
		[SerializeField] private Mesh[] meshes = null;

		private MeshFilter meshFilter;

		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			Randomize();
		}

		private Mesh RandomMesh
		{
			get { return meshes[UnityEngine.Random.Range(0, meshes.Length)]; }
		}

		public void Randomize()
		{
			meshFilter.sharedMesh = RandomMesh;
		}
	}
}