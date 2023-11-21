using System;
using UnityEngine;

namespace Aid.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int value = 100;

        [SerializeField] private int maxValue = 100;

        public int CurrentValue => value;
        public int MaxValue => maxValue;

        public float FillAmount => (float)value / maxValue;

        public bool IsAlive => value > 0;
        public bool IsDead => value <= 0;

        public event Action Died;
        public event Action Changed;

        public void Add(int amount)
        {
            AddInternal(amount);
        }

        public void Remove(int amount)
        {
            AddInternal(-amount);
        }

        private void AddInternal(int amount)
        {
            value += amount;
            value = Mathf.Clamp(value, 0, maxValue);
            
            Changed?.Invoke();

            if (value == 0) Died?.Invoke();
        }
    }
}