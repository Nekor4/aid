namespace Aid.StateMachine.Core
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Aid/States/New State Machine", order = 0)]
    public class StateMachine : ScriptableObject
    {
        [SerializeField] private State initState;
        private State _currentState;
        
        public State CurrentState => _currentState;

        public void Init()
        {
            _currentState = initState;
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState.TryGetTransition(out var transitionState))
                Transition(transitionState);

            _currentState.OnUpdate();
        }

        private void Transition(State transitionState)
        {
            _currentState.OnStateExit();
            _currentState = transitionState;
            _currentState.OnStateEnter();
        }
    }
}