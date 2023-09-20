using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aid
{
    public class TransitionProcessor : MonoBehaviour
    {
        private readonly List<Transition> _transitions = new List<Transition>(25);
        
        public Transition StartTransition(float duration, Transition.OnUpdate onUpdate, Action onComplete)
        {
            var transition = new Transition(duration, onUpdate, onComplete);
            _transitions.Add(transition);
            return transition;
        }
        public void StopTransition(Transition transition)
        {
            if (transition != null && _transitions.Contains(transition))
            {
                _transitions.Remove(transition);
            }
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;

            var toRemove = new List<Transition>();
            
            for (int i = 0; i < _transitions.Count; i++)
            {
                var transition = _transitions[i];
                transition.Update(deltaTime);

                if (transition.Progress >= 1)
                {
                    toRemove.Add(transition);
                }
            }

            for (int i = 0; i < toRemove.Count; i += 1)
            {
                toRemove[i].Complete();
                StopTransition(toRemove[i]);
            }
        }
    }
}