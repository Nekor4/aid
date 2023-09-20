using System;
using UnityEngine;

namespace Aid.StateMachine.Core
{
    [Serializable]
    public class StateTransition : IStateComponent
    {
        [SerializeField] private State targetState;
        [SerializeField] private ConditionsGroup conditionsGroup;

        public bool TryGetTransit(out State state)
        {
            state = ShouldTransition() ? targetState : null;
            return state != null;
        }

        public void OnStateEnter()
        {
            conditionsGroup.OnStateEnter();
        }

        public void OnStateExit()
        {
            conditionsGroup.OnStateExit();
        }

        private bool ShouldTransition()
        {
            return conditionsGroup.IsMet();
        }

        internal void ClearConditionsCache()
        {
            conditionsGroup.ClearCache();
        }
    }
}