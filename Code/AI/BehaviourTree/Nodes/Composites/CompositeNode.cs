using System.Collections.Generic;
using UnityEngine;

namespace Aid.AI.BehaviourTree.Nodes.Composites
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> children = new List<Node>();
        
        public override Node Clone()
        {
            var node =  Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }
    }
}