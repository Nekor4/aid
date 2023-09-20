using System;
using UnityEngine;

namespace Aid.StateMachine.Core
{
    public abstract class StateCondition : ScriptableObject, IStateComponent
    {
        [SerializeField] private bool cacheResult;

        private bool isCached, cachedStatement;
        protected abstract bool Statement();

        public void ClearStatementCache()
        {
            isCached = false;
        }

        public bool IsMet()
        {
            return GetStatement();
        }

        private bool GetStatement()
        {
            if (cacheResult == false)
                return Statement();

            if (isCached == false)
            {
                isCached = true;
                cachedStatement = Statement();
            }

            return cachedStatement;
        }

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateExit()
        {
        }
    }

    [Serializable]
    internal class Condition
    {
        public StateCondition stateCondition;
        public LogicOperator logicOperator;

        public enum LogicOperator
        {
            Or,
            And
        }
    }

    [Serializable]
    internal class ConditionsGroup : IStateComponent
    {
        public Condition[] conditions;

        public void OnStateEnter()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].stateCondition.OnStateEnter();
            }
        }

        public void OnStateExit()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].stateCondition.OnStateExit();
            }
        }

        public bool IsMet()
        {
            bool isAndFailed = false;

            for (int i = 0; i < conditions.Length; i++)
            {
                var condition = conditions[i];

                if (condition.logicOperator == Condition.LogicOperator.Or)
                {
                    if (isAndFailed)
                    {
                        isAndFailed = false;
                        continue;
                    }

                    return condition.stateCondition.IsMet();
                }
                else
                {
                    if (condition.stateCondition.IsMet())
                    {
                        continue;
                    }
                    else
                    {
                        isAndFailed = true;
                    }
                }
            }

            return isAndFailed == false;
        }

        public void ClearCache()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].stateCondition.ClearStatementCache();
            }
        }
    }
}