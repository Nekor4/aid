namespace Aid.UI
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Window Config", menuName = "Game/UI/Window Config", order = 0)]
    public class WindowConfig : ScriptableObject
    {
        [SerializeField] private GameObject prefab;

        private Window _window;
        private bool IsLoaded => _window != null;


        public event Action<Window> Shown
        {
            add => Window.Shown += value;
            remove => Window.Shown -= value;
        }
        
        
        public event Action<Window> Hidden
        {
            add => Window.Hidden += value;
            remove => Window.Hidden -= value;
        }

        public Window Window
        {
            get
            {
                if (IsLoaded == false) Load();
                return _window;
            }
        }

        public void Show()
        {
            if (IsLoaded == false) Load();
            WindowShowHideHandler.Instance.Show(_window);
        }

        public void Hide()
        {
            if (IsLoaded == false) Load();
            WindowShowHideHandler.Instance.Hide(_window);
        }

        private void Load()
        {
            _window = WindowsManager.Instance.Load(prefab);
        }
    }
}