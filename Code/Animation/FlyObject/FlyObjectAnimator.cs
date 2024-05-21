using System;
using Aid.Extensions;
using UnityEngine;

namespace Aid.Animation.FlyObject
{
    public class FlyObjectAnimator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem playParticle;
        [SerializeField] private TrailRenderer playTrail;

        private float _timer = 0;
        private Vector3 _startPoint;
        private Vector3 _controlPoint, _currentControlPointOffset, _localPositionInTarget;
        private Vector3 _endPoint;
        private Transform _targetPoint;

        private Vector3 _startScale, _endScale;

        private FlyObjectAnimation.FlySetting _settings;

        private Action _flyCompleted;

        private void Awake()
        {
            enabled = false;
        }

        public void Set(FlyObjectAnimation.FlySetting flySetting, Vector3 startPosition, Vector3 targetPosition,
            Action completed)
        {
            _settings = flySetting;
            _flyCompleted = completed;
            transform.position = startPosition;

            playTrail.enabled = true;
            playParticle.gameObject.SetActive(true);
            SetupFly(startPosition, targetPosition, Vector3.zero, Vector3.one);
        }

        private void SetupFly(Vector3 startPoint, Vector3 endPoint, Vector3 startScale, Vector3 endScale,
            Transform targetPoint = null, Action callback = null)
        {
            _currentControlPointOffset = _settings.RandomControlPointOffset;
            transform.localScale = startScale;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _startScale = startScale;
            _endScale = endScale;
            _targetPoint = targetPoint;
            _timer = 0;
            enabled = true;
        }

        private void Update()
        {
            _timer += Time.deltaTime / _settings.duration;
            _controlPoint = _startPoint + _currentControlPointOffset;

            if (_targetPoint != null)
                _endPoint = _targetPoint.position;

            transform.position = MathExtension.CubicBezier(_startPoint, _controlPoint, _endPoint,
                _settings.positionCurve.Evaluate(_timer));

            transform.localScale = Vector3.Lerp(_startScale, _endScale, _settings.scaleCurve.Evaluate(_timer));

            if (_timer >= 1)
            {
                OnFlyCompleted();
            }
        }

        private void OnFlyCompleted()
        {
            _targetPoint = null;
            enabled = false;
            _timer = 0;
            _flyCompleted?.Invoke();
        }
    }
}