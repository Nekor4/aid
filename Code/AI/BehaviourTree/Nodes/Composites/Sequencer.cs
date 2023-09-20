using System;

namespace Aid.BehaviourTree
{
    public class Sequencer : CompositeNode
    {

        private int currentChildIndex;
        protected override void OnStart()
        {
            currentChildIndex = 0;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {

            var child = children[currentChildIndex];
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;

                case State.Failure:
                    return State.Failure;
                    
                case State.Success:
                    currentChildIndex++;
                    break;
                
            }

             return currentChildIndex == children.Count ? State.Success : State.Running;
        }
    }
}