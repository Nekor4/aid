using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Aid
{
	public static class GameObjectExtensions
	{
		private const string ComponentIsNull = "Expected to find component of type {0} but found none at object {1}";

		public static T GetRequiredComponent<T>(this GameObject obj) where T : Component
		{
			var component = obj.GetComponent<T>();

			Assert.IsNotNull(component, string.Format(ComponentIsNull, typeof(T), obj));

			return component;
		}

		public static T GetRequiredComponentInChildren<T>(this GameObject obj, bool includeInavtice = true) where T : Component
		{
			var component = obj.GetComponentInChildren<T>(includeInavtice);

			Assert.IsNotNull(component, string.Format(ComponentIsNull, typeof(T), obj));

			return component;
		}

		public static T GetRequiredComponentInParent<T>(this GameObject obj) where T : Component
		{
			var component = obj.GetComponentInParent<T>();

			Assert.IsNotNull(component, string.Format(ComponentIsNull, typeof(T), obj));

			return component;
		}

		public static void SetLayerRecursively(this GameObject obj, int newLayer, Type type = null)
		{
			if (null == obj) return;


			if (type != null)
			{
				if (obj.GetComponent(type) == null)
				{
					obj.layer = newLayer;
				}
			}
			else
			{
				obj.layer = newLayer;
			}

			for (int i = 0; i < obj.transform.childCount; i++)
			{
				var child = obj.transform.GetChild(i);

				SetLayerRecursively(child.gameObject, newLayer, type);
			}
		}

		public static void SetStaticRecursively(this GameObject obj, bool isStatic)
		{
			if (null == obj) return;

				obj.isStatic = isStatic;

			for (int i = 0; i < obj.transform.childCount; i++)
			{
				var child = obj.transform.GetChild(i);

				SetStaticRecursively(child.gameObject, isStatic);
			}
		}
		
		public static void SetLayerRecursively(this List<GameObject> objects, int newLayer)
		{
			foreach (var gameObject in objects)
				gameObject.SetLayerRecursively(newLayer);
		}
	}
}