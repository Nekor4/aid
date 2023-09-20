using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Aid.UI
{
	public class WindowsRoot <T> : MonoBehaviour where T : MonoBehaviour
	{
		private const string SingletonIsNull = "An instance of {0} is needed in the scene, but there is none.";

		protected static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (T) FindObjectOfType(typeof(T));

					Assert.IsNotNull(instance, string.Format(SingletonIsNull, typeof(T)));
				}

				return instance;
			}
		}

		[SerializeField] private UIConfig config = null;

		private readonly LinkedList<Window> shownWindows = new LinkedList<Window>();

		public void Init()
		{
			ApplyConfig(gameObject, config);
			InitWindows();
			Initialize();
		}

		protected virtual void Initialize()
		{
			
		}
		
		public void HideAllWindows()
		{
			while (shownWindows.Count > 0)
			{
				var window = shownWindows.First.Value;
				window.Hidden -= OnWindowHidden;
				window.Shown += OnWindowShown;
				window.Hide();
				shownWindows.RemoveFirst();
			}
		}
		
		private void InitWindows()
		{
			var windows = gameObject.GetComponentsInChildren<Window>();
			var windowsCount = windows.Length;
			for (int i = 0; i < windowsCount; i++)
			{
					windows[i].Initialize();

				windows[i].Hide(false);
				
				windows[i].Shown += OnWindowShown;
			}
		}

		private void OnWindowShown(Window window)
		{
			shownWindows.AddLast(window);
			window.Shown -= OnWindowShown;
			window.Hidden += OnWindowHidden;
		}

		private void OnWindowHidden(Window window)
		{
			shownWindows.Remove(window);
			window.Shown += OnWindowShown;
			window.Hidden -= OnWindowHidden;
		}
		
		private static void ApplyConfig(GameObject gameObject, UIConfig config)
		{
			var canvas = gameObject.GetComponent<Canvas>();
			var scaler = gameObject.GetComponent<CanvasScaler>();

			canvas.planeDistance = config.planeDistance;
			scaler.referenceResolution = config.referenceResolution;
			scaler.uiScaleMode = config.uiScaleMode;
			scaler.screenMatchMode = config.screenMatchMode;
			scaler.matchWidthOrHeight = config.matchWidthOrHeight;
		}
		
	}
}