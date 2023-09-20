using System;
using UnityEngine;

namespace Aid
{
	public class TransformUtils
	{
		public static TransformCopy Copy(Transform transform)
		{
			var ret = new TransformCopy();

			ret.localPos = transform.localPosition;
			ret.localRotation = transform.localRotation;
			ret.localScale = transform.localScale;
			ret.parent = transform.parent;

			return ret;
		}

		public static WorldTransformCopy WorldCopy(Transform transform)
		{
			var ret = new WorldTransformCopy();

			ret.pos = transform.position;
			ret.rotation = transform.rotation;
			ret.scale = transform.localScale;
			ret.parent = transform.parent;

			return ret;
		}

		public static void CopyTo(TransformCopy origin, Transform destination)
		{
			destination.parent = origin.parent;
			destination.localPosition = origin.localPos;
			destination.localRotation = origin.localRotation;
			destination.localScale = origin.localScale;
		}

		public static void CopyTo(WorldTransformCopy origin, Transform destination)
		{
			destination.parent = origin.parent;
			destination.position = origin.pos;
			destination.rotation = origin.rotation;
			destination.localScale = origin.scale;
		}

		[Serializable]
		public struct TransformCopy
		{
			public Vector3 localPos;
			public Quaternion localRotation;
			public Vector3 localScale;
			public Transform parent;
		}

		[Serializable]
		public struct WorldTransformCopy
		{
			public Vector3 pos;
			public Quaternion rotation;
			public Vector3 scale;
			public Transform parent;
		}
	}
}