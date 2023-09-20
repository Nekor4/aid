using System.Collections.Generic;
using UnityEngine;

namespace Aid.UI
{
    public class WindowsRegistry
    {
        private readonly List<Window> registeredWindows;
        private readonly List<Window> shownWindows;

        public  WindowsRegistry()
        {
            registeredWindows = new List<Window>(32);
            shownWindows = new List<Window>(32);
        }

        public  void Register(Window window)
        {
            if (registeredWindows.Contains(window))
            {
                Debug.LogError($"Trying to register {window.name}, but it is already registered.");
                return;
            }

            window.Shown += OnWindowShown;
            window.Shown -= OnWindowHidden;

            registeredWindows.Add(window);
        }

        private  void OnWindowShown(Window window)
        {
            shownWindows.Add(window);
        }

        private  void OnWindowHidden(Window window)
        {
            shownWindows.Remove(window);
        }

        public  void HideAll()
        {
            for (int i = 0; i < shownWindows.Count; i++)
            {
                WindowShowHideHandler.Instance.Hide(shownWindows[i]);
            }            
        }
    }
}