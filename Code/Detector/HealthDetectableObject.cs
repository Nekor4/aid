using System;
using Aid.HealthComponents;
using UnityEngine;

namespace Aid.Detector
{
    public class HealthDetectableObject : MonoBehaviour, IDetectable
    {
        public bool IsDetectable => gameObject.activeSelf;
        public event Action<IDetectable> DetectionChanged;

        private Health _health;
        private State _state = State.NotReady;

        private enum State
        {
            NotReady,
            Ready,
            Registered,
            NotRegistered
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.Changed += Refresh;
            _state = State.Ready;
            Refresh();
        }

        private void OnDestroy()
        {
            _health.Changed -= Refresh;
        }


        private void OnEnable()
        {
            if (_state == State.NotReady) return;
            Register();
        }

        private void OnDisable()
        {
            if (_state == State.NotReady) return;
            UnRegister();
        }

        private void Refresh()
        {
            if (_state == State.NotReady) return;
            if (CanRegister())
                Register();
            else if (CanUnRegister())
                UnRegister();
        }

        private void Register()
        {
            DetectablesRegistry.Instance.Register(this);
            DetectionChanged?.Invoke(this);
            _state = State.Registered;
        }

        private bool CanRegister() => gameObject.activeSelf && _health.IsDead == false;

        private void UnRegister()
        {
            DetectablesRegistry.Instance.UnRegister(this);
            DetectionChanged?.Invoke(this);
            _state = State.NotRegistered;
        }

        private bool CanUnRegister() => _health.IsDead || gameObject.activeSelf == false;
    }
}