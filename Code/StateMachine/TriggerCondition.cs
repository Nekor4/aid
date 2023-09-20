using Aid.StateMachine.Core;
using UnityEngine;

namespace Aid.StateMachine
{
    [CreateAssetMenu(menuName = "Aid/States/Conditions/Trigger Condition")]
    public class TriggerCondition : StateCondition
    {
        [SerializeField] private Trigger trigger;

        private bool _canPass;

        public override void OnStateEnter()
        {
            _canPass = false;
            trigger.Triggered += OnTriggered;
        }

        private void OnTriggered()
        {
            _canPass = true;
        }

        public override void OnStateExit()
        {
            trigger.Triggered -= OnTriggered;
        }


        protected override bool Statement()
        {
            return _canPass;
        }
    }
}