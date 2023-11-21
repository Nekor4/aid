using System;
using System.Collections.Generic;
using Aid.Extensions;
using Aid.Transitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Aid.UI
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        public event Action<Window> Initialized, Hidden, Shown, StartShowing, StartHiding;

        private Transition _showTransition;
        private Transition _hideTransition;

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private GraphicRaycaster _raycaster;

        [SerializeField] private bool hideShowTransition = true;
        [SerializeField] private float transitionDuration = .25f;

        public enum State
        {
            Init,
            Hidden,
            Hiding,
            Shown,
            Showing
        }

        public bool IsHidden => CurrentState is State.Hidden or State.Hiding;

        public bool IsInitialized { get; private set; }

        public State CurrentState { get; private set; }

        protected void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _canvas = GetComponent<Canvas>();
            _raycaster = GetComponent<GraphicRaycaster>();
            _canvasGroup = GetComponent<CanvasGroup>();
            transform.ResetLocalValues();

            Init();
            Hide(false);
            IsInitialized = true;
            Initialized?.Invoke(this);
        }

        protected virtual void Init()
        {
        }

        #region Show

        public void Show()
        {
            Show(hideShowTransition);
        }

        public void Show(bool withTransition)
        {
            if (IsHidden == false) return;

            TransitionsManager.StopTransition(_showTransition);
            TransitionsManager.StopTransition(_hideTransition);

            CurrentState = State.Showing;
            StartShowing?.Invoke(this);
            OnStartShowing();

            enabled = true;
            _canvas.enabled = true;

            if (withTransition)
                StartShowTransition();
            else
                InternalOnShown();
        }

        private void StartShowTransition()
        {
            _showTransition = TransitionsManager.StartTransition(transitionDuration, ShowTransition, InternalOnShown);
        }

        private void ShowTransition(float progress)
        {
            _canvasGroup.alpha = Mathf.SmoothStep(0, 1, progress);
        }

        private void InternalOnShown()
        {
            CurrentState = State.Shown;
            _canvasGroup.alpha = 1;

            if (_raycaster != null)
                _raycaster.enabled = true;


            OnShown();
            Shown?.Invoke(this);
        }

        protected virtual void OnStartShowing()
        {
        }

        protected virtual void OnShown()
        {
        }

        #endregion

        #region Hide

        private void InternalHideWindow()
        {
            if (IsHidden) return;
            Hide(hideShowTransition);
        }

        public void Hide()
        {
            InternalHideWindow();
        }

        public void Hide(bool withTransition)
        {
            if (IsHidden) return;

            TransitionsManager.StopTransition(_showTransition);
            TransitionsManager.StopTransition(_hideTransition);

            CurrentState = State.Hiding;
            StartHiding?.Invoke(this);
            OnStartHiding();

            if (_raycaster != null)
                _raycaster.enabled = false;

            if (withTransition)
                StartHideTransition();
            else
                InternalOnHidden();
        }

        private void StartHideTransition()
        {
            _hideTransition = TransitionsManager.StartTransition(transitionDuration, HideTransition, InternalOnHidden);
        }

        private void HideTransition(float progress)
        {
            _canvasGroup.alpha = Mathf.SmoothStep(1, 0, progress);
        }

        private void InternalOnHidden()
        {
            CurrentState = State.Hidden;
            _canvasGroup.alpha = 0;
            Hidden?.Invoke(this);
            OnHidden();
            _canvas.enabled = false;
            enabled = false;
        }

        protected virtual void OnStartHiding()
        {
        }

        protected virtual void OnHidden()
        {
        }

        #endregion

        public void Toggle()
        {
            if (IsHidden) Show();
            else Hide();
        }
    }
}