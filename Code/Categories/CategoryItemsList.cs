using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aid.Categories
{
    public class CategoryItemsList<T> : MonoBehaviour where T : CategoryItem
    {
        private List<T> currentList;

        private CategoryItemListElement<T>[] items;

        private Action<T> clicked; 
        
        public void Set(List<T> templates, Action<T> onClicked)
        {
            items = GetComponentsInChildren<CategoryItemListElement<T>>();
            clicked = onClicked;
            currentList = templates;
            Refresh();
        }

        private void Refresh()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < currentList.Count; i++)
            {
                items[i].gameObject.SetActive(true);
                items[i].Set(currentList[i], OnItemClicked);
            }
        }

        private void OnItemClicked(T item)
        {
            clicked?.Invoke(item);
        }
    }
}