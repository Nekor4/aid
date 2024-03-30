using System.Threading.Tasks;
using UnityEngine;

namespace Aid.HealthComponents
{
    public class HealthBarPresenter : MonoBehaviour
    {
        [SerializeField] private float heightOffset = 2f;
        [SerializeField] private Color color = Color.green;
        private Health _health;
        private HealthBar _view;

        private async void OnEnable()
        {
            _health ??= GetComponent<Health>();

            _health.Died += Dispose;

            _view = HealthBarsPool.Instance.Get();
            await Task.Yield();
            _view.Set(_health, heightOffset, color);
        }

        private void OnDisable()
        {
            _health.Died -= Dispose;

            Dispose();
        }

        private void Dispose()
        {
            if (_view == null) return;

            if (HealthBarsPool.IsInstanceExists)
                HealthBarsPool.Instance.Release(_view);
            _view = null;
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
            if (_view != null)
                _view.ChangeColor(color);
        }
    }
}