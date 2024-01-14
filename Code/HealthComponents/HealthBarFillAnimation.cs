using UnityEngine;

namespace Aid.HealthComponents
{
    public class HealthBarFillAnimation
    {
        private const float ANIMATION_TIME = 0.5f;

        private float _currentFill, _targetFill, _startFill;

        private float _animationTimeLeft;

        public HealthBarFillAnimation(float currentFill)
        {
            _currentFill = currentFill;
        }

        public void Play(float targetFill)
        {
            _targetFill = targetFill;
            _startFill = _currentFill;
            _animationTimeLeft = ANIMATION_TIME;
        }

        public float Update(float deltaTime)
        {
            _animationTimeLeft -= deltaTime;
            var t = 1 - _animationTimeLeft / ANIMATION_TIME;
            _currentFill = Mathf.Lerp(_startFill, _targetFill, t);
            return _currentFill;
        }
    }
}