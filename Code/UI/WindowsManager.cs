namespace Aid.UI
{
	using UnityEngine;

	public class WindowsManager : MonoBehaviour
	{
		private static string PrefabPath = "UI/WindowsManager";
		
		protected static WindowsManager instance;

		public static WindowsManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (WindowsManager) FindObjectOfType(typeof(WindowsManager));

					if (instance == null)
					{
						CreateInstance();
					}
					instance.Init();
				}

				return instance;
			}
		}

		private static void CreateInstance()
		{
			instance = Instantiate(Resources.Load<WindowsManager>(PrefabPath));
		}

		private Canvas canvas;

		private Transform parent;

		private WindowsRegistry registry;

		private void Init()
		{
			DontDestroyOnLoad(gameObject);
			canvas = GetComponentInChildren<Canvas>();
			parent = canvas.transform;
			registry = new WindowsRegistry();
		}

		public Window Load(GameObject windowPrefab)
		{
			// var windowObject = Instantiate(windowPrefab);
			// DontDestroyOnLoad(windowObject);
			// windowObject.transform.parent = parent;
			// return windowObject.GetComponent<Window>();
			var window =Instantiate(windowPrefab, parent).GetComponent<Window>();
			registry.Register(window);
			return window;
		}

		public void HideAll()
		{
			registry.HideAll();
		}
	}
}
