using System.Threading.Tasks;
using UnityEngine;

namespace Aid.HealthComponents
{
    public class HealthBarPresenter : MonoBehaviour
    {
        [SerializeField] internal HealthBarFactorySO barFactory;
        [SerializeField] private float heightOffset = 2f;
        [SerializeField] private Color color = Color.green;
        private Health _health;
        private HealthBar _bar;

        private void OnEnable()
        {
            _health ??= GetComponent<Health>();

            HealthBarPresentersRegistry.Register(this);
        }

        internal void InjectHealthBar(HealthBar healthBar)
        {
            _bar = healthBar;
            _bar.Set(_health, heightOffset, color);
        }

        private void OnDisable()
        {
            HealthBarPresentersRegistry.Unregister(this);
        }

        internal void Dispose()
        {
            if (_bar == null) return;

            if (HealthBarsPool.IsInstanceExists)
                HealthBarsPool.Instance.Release(_bar, barFactory);
            _bar = null;
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
            if (_bar != null)
                _bar.ChangeColor(color);
        }
    }
}