﻿using System.Collections.Generic;
using UnityEngine;

namespace Aid.BehaviourTree
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