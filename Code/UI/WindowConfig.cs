namespace Aid.UI
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Window Config", menuName = "Aid/UI/Window Config", order = 0)]
    public class WindowConfig : ScriptableObject
    {
        [SerializeField] private Window prefab;

        public Window Prefab => prefab;

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

        public Window Window => WindowsManager.Instance.GetWindow(this);

        public void Show()
        {
            WindowShowHideHandler.Instance.Show(this);
        }

        public void Hide()
        {
            WindowShowHideHandler.Instance.Hide(this);
        }
    }
}