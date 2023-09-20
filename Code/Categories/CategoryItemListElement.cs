using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Aid.Categories
{
    public abstract class CategoryItemListElement<T> : MonoBehaviour, IPointerClickHandler where T : CategoryItem
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        
        public T CurrentItem { get; private set; }

        private Action<T> _clicked;

        public void Set(T item, Action<T> onClicked)
        {
            CurrentItem = item;
            _clicked = onClicked;
            icon.sprite = item.icon;
            nameText.text = item.displayName;
            OnSet(item);
        }

        protected abstract void OnSet(T item);

        public void OnPointerClick(PointerEventData eventData)
        {
            if(CanClick() == false) return;
            _clicked?.Invoke(CurrentItem);
            OnClicked();
        }

        protected abstract bool CanClick();

        protected abstract void OnClicked();
    }
}