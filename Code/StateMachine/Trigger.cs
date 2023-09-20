using System;
using UnityEngine;

namespace Aid.StateMachine
{
    [CreateAssetMenu(menuName = "Aid/States/Others/Trigger")]
    public class Trigger : ScriptableObject
    {
        public event Action Triggered;
        public void Invoke()
        {
            Triggered?.Invoke();
        }
    }
}