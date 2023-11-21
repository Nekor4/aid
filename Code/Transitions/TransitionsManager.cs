using System;
using UnityEngine;

namespace Aid.Transitions
{
    using Object = UnityEngine.Object;

    public static class TransitionsManager 
    {
        private static TransitionProcessor _processor;

        private static TransitionProcessor Processor
        {
            get
            {
                if (_processor == null)
                {
                   _processor = Object.FindObjectOfType<TransitionProcessor>();

                   if (_processor == null)
                       CreateProcessor();
                }

                return _processor;
            }   
        }

        private static void CreateProcessor()
        {
            var processorObj = new GameObject("TransitionsProcessor");
            _processor = processorObj.AddComponent<TransitionProcessor>();
            Object.DontDestroyOnLoad(processorObj);
        }
        
        public static Transition StartTransition(float duration, Transition.OnUpdate onUpdate, Action onComplete)
        {
            return Processor.StartTransition(duration, onUpdate, onComplete);
        }

        public static void StopTransition(Transition transition)
        {
            Processor.StopTransition(transition);
        }
    }
}