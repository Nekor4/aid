using System.Collections.Generic;
using UnityEngine;

namespace Aid.UI
{
    public class WindowsRegistry
    {
        private readonly Dictionary<WindowConfig, Window> _registeredWindows = new(32);
        private readonly List<Window> _shownWindows = new(32);

        private Transform _parent;
        public WindowsRegistry(Transform parent)
        {
            _parent = parent;
            var windows = parent.GetComponentsInChildren<Window>(true);
            foreach (var window in windows)
            {
                Register(window);
            }
        }

        public void Register(Window window)
        {
            if(window.Config == null)
            {
                Debug.LogError($"Trying to register {window.name}, but it has no config.");
                return;
            }
            
            if (_registeredWindows.ContainsKey(window.Config))
            {
                Debug.LogError($"Trying to register {window.name}, but it is already registered with config {window.Config.name}.");
                return;
            }

            window.Shown += OnWindowShown;
            window.Shown -= OnWindowHidden;

            _registeredWindows.Add(window.Config, window);
        }
        
        public Window GetWindow(WindowConfig config) 
        {
            if (_registeredWindows.TryGetValue(config, out var window))
            {
                return window;
            }

            return Load(config);
        }
        
        public Window Load(WindowConfig config)
        {
            if (config == null)
            {
                Debug.LogError("WindowConfig is null.");
                return null;
            }

            if (_registeredWindows.TryGetValue(config, out var window))
            {
                return window;
            }

            if (config.Prefab == null)
            {
                Debug.LogError($"WindowConfig {config.name} has no prefab assigned.", config);
                return null;
            }

            window = Object.Instantiate(config.Prefab, _parent);
            _registeredWindows.Add(config, window);
            return window;
        }
  

        private void OnWindowShown(Window window)
        {
            _shownWindows.Add(window);
        }

        private  void OnWindowHidden(Window window)
        {
            _shownWindows.Remove(window);
        }

        public  void HideAll()
        {
            for (int i = 0; i < _shownWindows.Count; i++)
            {
                WindowShowHideHandler.Instance.Hide(_shownWindows[i].Config);
            }            
        }
    }
}