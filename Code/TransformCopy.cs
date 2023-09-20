using UnityEngine;
using System.Collections;

namespace Aid
{
	public struct TransformCopy
	{
		public readonly Vector3 localPosition;
		public readonly Quaternion localRotation;
		public readonly Vector3 localScale;

		public TransformCopy(Transform transform)
		{
			localPosition = transform.localPosition;
			localRotation = transform.localRotation;
			localScale = transform.localScale;
		}
	}
}