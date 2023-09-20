using UnityEngine;

namespace Aid.StateMachine.Core
{
    public abstract class StateAction : ScriptableObject, IStateComponent
    {
        public abstract void OnUpdate();
        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }
    }
}