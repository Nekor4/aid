namespace Aid.BehaviourTree
{
    using UnityEngine;

    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success
        }

        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 graphPosition;
        [HideInInspector] public Blackboard blackboard;
        [HideInInspector] public GameObject runnerObject;
        [TextArea] public string description;

        public State CurrentState { get; private set; } = State.Running;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            CurrentState = OnUpdate();

            if (CurrentState != State.Running)
            {
                OnStop();
                started = false;
            }

            return CurrentState;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public void Abort()
        {
            BehaviourTree.Traverse(this, (node) =>
            {
                node.started = false;
                node.CurrentState = State.Running;
                node.OnStop();
            });
        }

        public virtual void OnDrawGizmos()
        {
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}