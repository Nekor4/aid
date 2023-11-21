using UnityEngine;

namespace Aid.AI.BehaviourTree.Nodes.Decorators
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node child;

        public override Node Clone()
        {
            var node = Instantiate(this);
            if (child != null)
            {
                node.child = child.Clone();
            }

            return node;
        }
    }
}