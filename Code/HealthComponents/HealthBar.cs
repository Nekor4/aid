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

        private HealthBarFillAnimation _fillAnimation;
        private DynamicUiElement _dynamicUiElement;
        private Health _health;
        private float _heightOffset;

        private void Awake()
        {
            _fillAnimation = new HealthBarFillAnimation(1);
            _dynamicUiElement = GetComponent<DynamicUiElement>();
        }

        public void Set(Health health, float heightOffset, Color color)
        {
            _heightOffset = heightOffset;
            _health = health;
            valueFill.color = color;
            Redraw();
            _health.Changed += HealthOnChanged;
        }

        public void Clear()
        {
            _health.Changed -= HealthOnChanged;
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
            var targetFill = _health.FillAmount;
            valueFill.fillAmount = targetFill;
            _fillAnimation.Play(targetFill);
        }

        private void Update()
        {
            valueFillAfterimage.fillAmount = _fillAnimation.Update(Time.deltaTime);
        }

        public void ChangeColor(Color newColor)
        {
            valueFill.color = newColor;
        }
    }
}