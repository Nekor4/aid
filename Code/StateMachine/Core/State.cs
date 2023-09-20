using System;
using System.Collections.Generic;
using Aid.StateMachine.Core;
using UnityEngine;

namespace Aid.StateMachine.Core
{
    [CreateAssetMenu(menuName = "Aid/States/New State", order = 0)]
    public class State : ScriptableObject
    {
        [SerializeField] private List<StateTransition> transitions;
        [SerializeField] private List<StateAction> actions;

        private void OnEnable()
        {
            RemoveNullActions();
        }

        private void RemoveNullActions()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] == null)
                {
                    actions.RemoveAt(i);
                    i--;
                }
            }
        }

        public void OnStateEnter()
        {
            void LocalOnStateEnter(IReadOnlyList<IStateComponent> comps)
            {
                for (int i = 0; i < comps.Count; i++)
                    comps[i].OnStateEnter();
            }

            LocalOnStateEnter(transitions);
            LocalOnStateEnter(actions);
        }

        public void OnUpdate()
        {
            for (int i = 0; i < actions.Count; i++)
                actions[i].OnUpdate();
        }

        public void OnStateExit()
        {
            void OnLocalStateExit(IReadOnlyList<IStateComponent> comps)
            {
                for (int i = 0; i < comps.Count; i++)
                    comps[i].OnStateExit();
            }

            OnLocalStateExit(transitions);
            OnLocalStateExit(actions);
        }

        public bool TryGetTransition(out State state)
        {
            state = null;

            for (int i = 0; i < transitions.Count; i++)
                if (transitions[i].TryGetTransit(out state))
                    break;

            for (int i = 0; i < transitions.Count; i++)
                transitions[i].ClearConditionsCache();

            return state != null;
        }
    }
}