using Aid.StateMachine.Core;
using UnityEditor;
using UnityEngine;

namespace Aid.StateMachine
{
    [CustomEditor(typeof(StateMachineRunner))]
    public class StateMachineRunnerEditor : Editor
    {
        private StateMachineRunner _stateMachineRunner;

        private void OnEnable()
        {
            _stateMachineRunner = target as StateMachineRunner;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_stateMachineRunner.StateMachine != null)
                GUILayout.Label("Current State: " + _stateMachineRunner.StateMachine.CurrentState.name);
        }
    }
}