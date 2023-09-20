using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aid.Categories
{
    public class CategoryListElement<T> : MonoBehaviour, IPointerClickHandler
    {
        
        
        public T CurrentCategory { get; private set; }
        
        private Action< CategoryListElement<T>> clicked;

        public void Set(T category, Action< CategoryListElement<T>> onClicked)
        {
            clicked = onClicked;
            CurrentCategory = category;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            clicked?.Invoke(this);
        }
    }
}