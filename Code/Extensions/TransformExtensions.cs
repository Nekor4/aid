using UnityEngine;
using System.Collections.Generic;

namespace Aid
{
	public static class TransformExtensions
	{
		public static void Set(this Transform transform, TransformCopy copy)
		{
			transform.localPosition = copy.localPosition;
			transform.localRotation = copy.localRotation;
			transform.localScale = copy.localScale;
		}

		public static TransformCopy GetCopy(this Transform transform)
		{
			return new TransformCopy(transform);
		}

		public static void ResetLocalPositionAndRotation(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}

		public static List<Transform> GetChildren(this Transform transform)
		{
			var children = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++)
				children.Add(transform.GetChild(i));

			return children;
		}

		public static void SetValues(this Transform transform, Transform otherTransform)
		{
			transform.parent = otherTransform.parent;
			transform.position = otherTransform.position;
			transform.rotation = otherTransform.rotation;
			transform.localScale = otherTransform.localScale;
		}

		public static void ResetLocalValues(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}

		public static void SetPosX(this Transform transform, float x)
		{
			var position = transform.position;
			position = new Vector3(x, position.y, position.z);
			transform.position = position;
		}

		public static void SetPosY(this Transform transform, float y)
		{
			var position = transform.position;
			position = new Vector3(position.x, y, position.z);
			transform.position = position;
		}

		public static void SetPosZ(this Transform transform, float z)
		{
			var position = transform.position;
			position = new Vector3(position.x, position.y, z);
			transform.position = position;
		}

		public static void SetLocalPosX(this Transform transform, float x)
		{
			var localPosition = transform.localPosition;
			localPosition = new Vector3(x, localPosition.y, localPosition.z);
			transform.localPosition = localPosition;
		}

		public static void SetLocalPosY(this Transform transform, float y)
		{
			var localPosition = transform.localPosition;
			localPosition = new Vector3(localPosition.x, y, localPosition.z);
			transform.localPosition = localPosition;
		}

		public static void SetLocalPosZ(this Transform transform, float z)
		{
			var localPosition = transform.localPosition;
			localPosition = new Vector3(localPosition.x, localPosition.y, z);
			transform.localPosition = localPosition;
		}

		public static void SetLocalScaleX(this Transform transform, float x)
		{
			var localScale = transform.localScale;
			localScale = new Vector3(x, localScale.y, localScale.z);
			transform.localScale = localScale;
		}

		public static void SetLocalScaleY(this Transform transform, float y)
		{
			var localScale = transform.localScale;
			localScale = new Vector3(localScale.x, y, localScale.z);
			transform.localScale = localScale;
		}

		public static void SetLocalScaleZ(this Transform transform, float z)
		{
			var localScale = transform.localScale;
			localScale = new Vector3(localScale.x, localScale.y, z);
			transform.localScale = localScale;
		}

		public static void SetLocalEulerX(this Transform transform, float x)
		{
			var localEuler = transform.localEulerAngles;
			localEuler = new Vector3(x, localEuler.y, localEuler.z);
			transform.localEulerAngles = localEuler;
		}

		public static void SetLocalEulerY(this Transform transform, float y)
		{
			var localEuler = transform.localEulerAngles;
			localEuler = new Vector3(localEuler.x, y, localEuler.z);
			transform.localEulerAngles = localEuler;
		}

		public static void SetLocalEulerZ(this Transform transform, float z)
		{
			var localEuler = transform.localEulerAngles;
			localEuler = new Vector3(localEuler.x, localEuler.y, z);
			transform.localEulerAngles = localEuler;
		}
	}
}