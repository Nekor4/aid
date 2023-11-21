using Aid.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aid.HealthComponents
{
    [RequireComponent(typeof(DynamicUiElement))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image valueFill;
        [SerializeField] private Image valueFillAfterimage;
        [SerializeField] private TextMeshProUGUI valueText;

        private DynamicUiElement _dynamicUiElement;
        private Health _health;
        private float _heightOffset;

        private void Awake()
        {
            _dynamicUiElement = GetComponent<DynamicUiElement>();
        }

        public void Set(Health health, float heightOffset)
        {
            _heightOffset = heightOffset;
            _health = health;
            Redraw();
            _health.Changed += HealthOnChanged;
        }

        public void Clear()
        {
            _health = null;
        }

        private void Redraw()
        {
            var fill = _health.FillAmount;
            valueFill.fillAmount = fill;
            valueFillAfterimage.fillAmount = fill;
            valueText.text = _health.CurrentValue.ToString();
        }

        private void HealthOnChanged()
        {
            AnimateRedraw();
        }


        private void LateUpdate()
        {
            _dynamicUiElement.UpdatePosition(_health.transform.position + _health.transform.up * _heightOffset);
        }
        


        private void AnimateRedraw()
        {
            valueText.text = _health.CurrentValue.ToString();
            var startFill = valueFill.fillAmount;
            var targetFill = _health.FillAmount;
            valueFill.fillAmount = targetFill;

            valueFillAfterimage.fillAmount = targetFill;
        }
    }
}