using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aid.BehaviourTree.Editor
{
    using UnityEditor.Experimental.GraphView;
    using UnityEditor;
    using UnityEngine.UIElements;

    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits>
        {
        }

        public Action<NodeView> Selected;

        private BehaviourTree currentTree;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new DoubleClickSelection());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    $"{BehaviourTreeEditor.AssetsPath}/Editor/AI/BehaviourTree/Editor/UI/BehaviourTreeEditor.uss");

            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            PopulateView(currentTree);
            AssetDatabase.SaveAssets();
        }

        public NodeView FindNodeView(Aid.BehaviourTree.Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        public void DrawEmpty()
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
        }

        public void PopulateView(BehaviourTree tree)
        {
            currentTree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (tree.rootNode == null)
            {
                tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            ValidateTree();

            //Creates node view
            tree.nodes.ForEach(CreateNodeView);

            //Create edges
            tree.nodes.ForEach(n =>
            {
                var children = BehaviourTree.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                });
            });
        }

        private void ValidateTree()
        {
            for (int i = 0; i < currentTree.nodes.Count; i++)
            {
                var node = currentTree.nodes[i];
                if (node == null)
                {
                    currentTree.nodes.RemoveAt(i);
                    i--;
                    continue;
                }

                if (node is CompositeNode compositeNode)
                {
                    for (int j = 0; j < compositeNode.children.Count; j++)
                    {
                        if (compositeNode.children[j] == null)
                        {
                            compositeNode.children.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
        {
            if (graphviewchange.elementsToRemove != null)
            {
                graphviewchange.elementsToRemove.ForEach(e =>
                {
                    if (e is NodeView nodeView)
                    {
                        currentTree.DeleteNode(nodeView.node);
                    }
                    else if (e is Edge edge)
                    {
                        var parentView = edge.output.node as NodeView;
                        var childView = edge.input.node as NodeView;
                        currentTree.RemoveChild(parentView.node, childView.node);
                    }
                });
            }

            if (graphviewchange.edgesToCreate != null)
            {
                graphviewchange.edgesToCreate.ForEach(edge =>
                {
                    var parentView = edge.output.node as NodeView;
                    var childView = edge.input.node as NodeView;
                    currentTree.AddChild(parentView.node, childView.node);
                });
            }

            if (graphviewchange.movedElements != null)
            {
                nodes.ForEach(n =>
                {
                    var view = n as NodeView;
                    view.SortChildren();
                });
            }

            return graphviewchange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var localMousePosition = evt.localMousePosition;
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}",
                        a => CreateNode(type, localMousePosition));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}",
                        a => CreateNode(type, localMousePosition));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}",
                        a => CreateNode(type, localMousePosition));
                }
            }
        }


        private void CreateNode(System.Type type, Vector2 position)
        {
            var node = currentTree.CreateNode(type);
            node.graphPosition = viewTransform.matrix.inverse.MultiplyPoint(position);
            CreateNodeView(node);
        }

        private void CreateNodeView(Aid.BehaviourTree.Node node)
        {
            var nodeView = new NodeView(node);
            nodeView.Selected = Selected;
            AddElement(nodeView);
        }

        public void UpdateNodeState()
        {
            nodes.ForEach(n =>
            {
                var view = n as NodeView;
                view.UpdateState();
            });
        }
    }
}