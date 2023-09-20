using UnityEngine;

namespace Aid.StateMachine.Core
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;
        
        public StateMachine StateMachine => stateMachine;
        private void Awake()
        {
            stateMachine = Instantiate(stateMachine);
            
            Application.targetFrameRate = 60;
            stateMachine.Init();
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}