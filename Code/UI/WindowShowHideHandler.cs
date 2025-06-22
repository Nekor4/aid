using System.Collections.Generic;
using UnityEngine;

namespace Aid.UI
{
    public class WindowShowHideHandler : MonoBehaviour
    {
        private readonly List<WindowConfig> _windowsToShow = new List<WindowConfig>(32);
        private readonly List<WindowConfig> _windowsToHide = new List<WindowConfig>(32);

        private static WindowShowHideHandler _instance;

        public static WindowShowHideHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("WindowShowHideHandler").AddComponent<WindowShowHideHandler>();
                }

                return _instance;
            }
        }
        public void Show(WindowConfig window)
        {
            if (_windowsToShow.Contains(window)) return;

            if (_windowsToHide.Contains(window))
                _windowsToHide.Remove(window);

            _windowsToShow.Add(window);
        }

        public void Hide(WindowConfig window)
        {
            if (_windowsToHide.Contains(window)) return;

            if (_windowsToShow.Contains(window))
                _windowsToShow.Remove(window);

            _windowsToHide.Add(window);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _windowsToShow.Count; i++)
            {
               WindowsManager.Instance.GetWindow(_windowsToShow[i]).Show();
            }

            for (int i = 0; i < _windowsToHide.Count; i++)
            {
                WindowsManager.Instance.GetWindow(_windowsToHide[i]).Hide();
            }

            _windowsToShow.Clear();
            _windowsToHide.Clear();
        }
    }
}