﻿using System;
using Aid.AI.BehaviourTree.Nodes;
using Aid.AI.BehaviourTree.Nodes.Actions;
using Aid.AI.BehaviourTree.Nodes.Composites;
using Aid.AI.BehaviourTree.Nodes.Decorators;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Node = Aid.AI.BehaviourTree.Nodes.Node;

namespace Aid.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> Selected;
        public Node node;
        public Port input;
        public Port output;

        public NodeView(Node node) : base($"{BehaviourTreeEditor.AssetsPath}//Editor/AI/BehaviourTree/Editor/UI/NodeView.uxml")
        {
            this.node = node;
            this.node.name = node.GetType().Name;
            this.title = node.name.Replace("(Clone)", "").Replace("Node", "");
            this.viewDataKey = node.guid;

            style.left = node.graphPosition.x;
            style.top = node.graphPosition.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();
            SetupDataBinding();
        }

        private void SetupDataBinding()
        {
            Label descriptionLabel = this.Q<Label>("description");
            descriptionLabel.bindingPath = "description";
            descriptionLabel.Bind(new SerializedObject(node));
        }

        private void SetupClasses()
        {
            if (node is ActionNode)
            {
                AddToClassList("action");
            }
            else if (node is CompositeNode)
            {
                AddToClassList("composite");
            }
            else if (node is DecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if (node is RootNode)
            {
                AddToClassList("root");
            }
        }

        private void CreateInputPorts()
        {
            if (node is ActionNode)
            {
                input = new NodePort(Direction.Input, Port.Capacity.Single);
            }
            else if (node is CompositeNode)
            {
                input = new NodePort(Direction.Input, Port.Capacity.Single);
            }
            else if (node is DecoratorNode)
            {
                input = new NodePort(Direction.Input, Port.Capacity.Single);
            }
            else if (node is RootNode)
            {
            }

            if (input != null)
            {
                input.portName = "";
                input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            if (node is ActionNode)
            {
            }
            else if (node is CompositeNode)
            {
                output = new NodePort(Direction.Output, Port.Capacity.Multi);
            }
            else if (node is DecoratorNode)
            {
                output = new NodePort(Direction.Output, Port.Capacity.Single);
            }
            else if (node is RootNode)
            {
                output = new NodePort(Direction.Output, Port.Capacity.Single);
            }

            if (output != null)
            {
                output.portName = "";
                output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "Behaviour Tree (Set Position");
            node.graphPosition.x = newPos.xMin;
            node.graphPosition.y = newPos.yMin;
            EditorUtility.SetDirty(node);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            Selected?.Invoke(this);
        }

        public void SortChildren()
        {
            if (node is CompositeNode composite)
            {
                composite.children.Sort(SortByHorizontalPosition);
            }
        }

        private int SortByHorizontalPosition(Node left, Node right)
        {
            return left.graphPosition.x < right.graphPosition.x ? -1 : 1;
        }

        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("failure");
            RemoveFromClassList("success");

            if (Application.isPlaying)
            {
                switch (node.CurrentState)
                {
                    case Node.State.Running:
                        if (node.started)
                        {
                            AddToClassList("running");
                        }

                        break;
                    case Node.State.Failure:
                        AddToClassList("failure");
                        break;
                    case Node.State.Success:
                        AddToClassList("success");
                        break;
                }
            }
        }
    }
}