namespace Aid.StateMachine.Core
{
    public interface IStateComponent
    {
        void OnStateEnter();
        void OnStateExit();
    }
}