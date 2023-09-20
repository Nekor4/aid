using System.Collections.Generic;
using UnityEngine;

namespace Aid.Categories
{
    public abstract class CategoryItemsCollection<T> : ScriptableObject where T : CategoryItem
    {
        public abstract List<T> Items { get; }

        public T GetTemplateByName(string templateName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].name == templateName)
                {
                    return Items[i];
                }
            }

            return null;
        }
    }
}
