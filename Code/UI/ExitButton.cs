using UnityEngine;
using UnityEngine.UI;

namespace Aid.UI
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        private Window _window;
        private Button _button;

        private void Awake()
        {
            _window = GetComponentInParent<Window>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _window.Hide();
        }
    }
}