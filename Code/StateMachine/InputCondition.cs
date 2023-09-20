using Aid.StateMachine.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Aid.StateMachine
{
    [CreateAssetMenu(menuName = "Aid/States/Conditions/Input Condition")]
    public class InputCondition : StateCondition
    {
        [SerializeField] private InputActionReference inputAction;

        private bool isPerformed;

        public override void OnStateEnter()
        {
            inputAction.action.Enable();
            inputAction.action.performed += OnPerformed;
        }

        public override void OnStateExit()
        {
            inputAction.action.Disable();
            inputAction.action.performed -= OnPerformed;
        }

        private void OnPerformed(InputAction.CallbackContext callbackContext)
        {
            isPerformed = true;
        }

        protected override bool Statement()
        {
            var value = isPerformed;
            isPerformed = false;
            return value;
        }
    }
}