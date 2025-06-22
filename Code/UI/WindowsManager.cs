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
                    instance = (WindowsManager)FindAnyObjectByType(typeof(WindowsManager));

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

        private Canvas _canvas;

        private Transform _parent;

        private WindowsRegistry _registry;

        private void Init()
        {
            DontDestroyOnLoad(gameObject);
            _canvas = GetComponentInChildren<Canvas>();
            _parent = _canvas.transform;
            _registry = new WindowsRegistry(_parent);
        }

        public void HideAll()
        {
            _registry.HideAll();
        }

        public Window GetWindow(WindowConfig windowConfig)
        {
            return _registry.GetWindow(windowConfig);
        }
    }
}