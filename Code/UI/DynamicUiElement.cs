using UnityEngine;

namespace Aid.UI
{
    public class DynamicUiElement : MonoBehaviour
    {
        [SerializeField] private bool clampToScreenEdge;
        [SerializeField] private float screenOffset = .05f;
        [SerializeField] private bool smoothMovement = false;

        private Camera mainCamera;
        protected RectTransform rectTransform;

        private bool isInited;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            mainCamera = Camera.main;

            Init();
            isInited = true;
        }

        protected virtual void Init()
        {
        }

        public void UpdatePosition(Vector3 worldPosition)
        {
            if (isInited == false) return;

            var newAnchor = mainCamera.WorldToViewportPoint(worldPosition);

            if (clampToScreenEdge)
            {
                var min = 0 + screenOffset;
                var max = 1 - screenOffset;

                newAnchor.x = Mathf.Clamp(newAnchor.x, min, max);
                newAnchor.y = Mathf.Clamp(newAnchor.y, min, max);
            }

            if (smoothMovement)
            {
                newAnchor = Vector3.Lerp(rectTransform.anchorMin, newAnchor, .1f);
            }

            rectTransform.anchorMin = newAnchor;
            rectTransform.anchorMax = newAnchor;
        }

        public void ForcePosition(Vector3 worldPosition)
        {
            var newAnchor = mainCamera.WorldToViewportPoint(worldPosition);
            rectTransform.anchorMin = newAnchor;
            rectTransform.anchorMax = newAnchor;
        }
    }
}