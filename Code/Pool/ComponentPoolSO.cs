using UnityEngine;

namespace Aid.Pool
{
	/// <summary>
	/// Implements a Pool for Component types.
	/// </summary>
	/// <typeparam name="T">Specifies the component to pool.</typeparam>
	public abstract class ComponentPoolSO<T> : PoolSO<T> where T : Component
	{
		private Transform _poolRoot;

		public Transform PoolRoot
		{
			get
			{
				if (_poolRoot == null)
				{
					if (parent is RectTransform)
					{
						var rectTransform = new GameObject(name).AddComponent<RectTransform>();

						_poolRoot = rectTransform;
					}
					else
					{
						_poolRoot = new GameObject(name).transform;
					}

					_poolRoot.SetParent(parent);
				}

				return _poolRoot;
			}
		}

		private Transform parent;

		/// <summary>
		/// Parents the pool root transform to <paramref name="t"/>.
		/// </summary>
		/// <param name="t">The Transform to which this pool should become a child.</param>
		/// <remarks>NOTE: Setting the parent to an object marked DontDestroyOnLoad will effectively make this pool DontDestroyOnLoad.<br/>
		/// This can only be circumvented by manually destroying the object or its parent or by setting the parent to an object not marked DontDestroyOnLoad.</remarks>
		public void SetParent(Transform t)
		{
			parent = t;
			PoolRoot.SetParent(parent);

			if (parent is RectTransform parentRect)
			{
				_poolRoot.ResetLocalValues();
				var rectTransform = (RectTransform)_poolRoot;
				rectTransform.anchorMin = parentRect.anchorMin;
				rectTransform.anchorMax = parentRect.anchorMax;
				rectTransform.anchoredPosition = parentRect.anchoredPosition;
				rectTransform.sizeDelta = parentRect.sizeDelta;
			}
		}

		public override T Request()
		{
			T member = base.Request();
			member.gameObject.SetActive(true);
			return member;
		}

		public override void Return(T member)
		{
			member.transform.SetParent(PoolRoot.transform);
			member.gameObject.SetActive(false);
			base.Return(member);
		}

		protected override T Create()
		{
			T newMember = base.Create();
			newMember.transform.SetParent(PoolRoot.transform);
			newMember.gameObject.SetActive(false);
			return newMember;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			if (_poolRoot != null)
			{
#if UNITY_EDITOR
				DestroyImmediate(_poolRoot.gameObject);
#else
				Destroy(_poolRoot.gameObject);
#endif
			}
		}
	}
}
