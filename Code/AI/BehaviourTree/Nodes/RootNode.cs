using UnityEngine;

namespace Aid.BehaviourTree
{
    public class RootNode : Node
    {
        [HideInInspector] public Node child;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return child.Update();
        }

        public override Node Clone()
        {
            var node = Instantiate(this);
            
            if (child != null)
                node.child = child.Clone();
            
            return node;
        }
    }
}