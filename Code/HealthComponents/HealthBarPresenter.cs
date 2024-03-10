using UnityEngine;

namespace Aid.HealthComponents
{
    public class HealthBarPresenter : MonoBehaviour
    {
        [SerializeField] private float heightOffset = 2f;
        [SerializeField] private Color color = Color.green;
        private Health _health;
        private HealthBar _view;

        private void OnEnable()
        {
            _health ??= GetComponent<Health>();

            _view = HealthBarsPool.Instance.Get();
            _view.Set(_health, heightOffset, color);
        }

        private void OnDisable()
        {
            if (HealthBarsPool.IsInstanceExists)
                HealthBarsPool.Instance.Release(_view);
            _view = null;
        }
    }
}