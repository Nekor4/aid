using UnityEngine;

namespace Aid.BehaviourTree
{
    public class DebugLogNode : ActionNode
    {

        public string message;
        protected override void OnStart()
        {
            if(blackboard.TryGetParam<StringParam>("TestParam", out var param))
            {
                Debug.Log("Test: " + param.value);
            }
            
            Debug.Log($"OnStart: {message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop: {message}");
        }

        protected override State OnUpdate()
        {
            Debug.Log($"OnUpdate: {message}");
            return State.Success;
        }
    }
}