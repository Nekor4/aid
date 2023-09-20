using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Aid.BehaviourTree {
    [CreateAssetMenu(menuName = "Aid/AI/Behaviour Tree")]
    public class BehaviourTree : ScriptableObject {
        public Node rootNode;
        public Node.State treeState = Node.State.Running;
        public List<Node> nodes = new List<Node>();

        [SerializeField] private Blackboard blackboard;
        public Blackboard Blackboard => blackboard;

        public Node.State Update() {
            if (rootNode.CurrentState == Node.State.Running) {
                treeState = rootNode.Update();
            }
            return treeState;
        }

        public static List<Node> GetChildren(Node parent) {
            List<Node> children = new List<Node>();

            if (parent is DecoratorNode decorator && decorator.child != null) {
                children.Add(decorator.child);
            }

            if (parent is RootNode rootNode && rootNode.child != null) {
                children.Add(rootNode.child);
            }

            if (parent is CompositeNode composite) {
                return composite.children;
            }

            return children;
        }

        public static void Traverse(Node node, System.Action<Node> visiter) {
            if (node) {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        public BehaviourTree Clone(string runnerName = "") {
            BehaviourTree tree = Instantiate(this);
            tree.name = $"{name}-{runnerName}";
            tree.blackboard = tree.blackboard.Clone();
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, (n) => {
                tree.nodes.Add(n);
            });

            return tree;
        }

        public void Bind(GameObject runnerObject) {
            Traverse(rootNode, node => {
                node.blackboard = Blackboard;
                node.runnerObject = runnerObject;
            });
        }


        #region Editor Compatibility
#if UNITY_EDITOR

        private void OnValidate()
        {   
            var isDirty = false;
            if (blackboard == null)
            {
                blackboard = CreateInstance<Blackboard>();
                blackboard.name = "Blackboard";
                AssetDatabase.AddObjectToAsset(blackboard, this);
                isDirty = true;
  
            }
            
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null)
                {
                    nodes.RemoveAt(i);
                    i--;
                    isDirty = true;
                }
            }

            if (isDirty)
            {
                EditorUtility.SetDirty(this);
            }
        }
        
        public Node CreateNode(System.Type type) {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying) {
                AssetDatabase.AddObjectToAsset(node, this);
            }

            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");

            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node) {
            Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
            nodes.Remove(node);

            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);

            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child) {
            if (parent is DecoratorNode decorator) {
                Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
            }

            if (parent is RootNode rootNode) {
                Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
            }

            if (parent is CompositeNode composite) {
                Undo.RecordObject(composite, "Behaviour Tree (AddChild)");
                composite.children.Add(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public void RemoveChild(Node parent, Node child) {
            if (parent is DecoratorNode decorator) {
                Undo.RecordObject(decorator, "Behaviour Tree (RemoveChild)");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
            }

            if (parent is RootNode rootNode) {
                Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
            }

            if (parent is CompositeNode composite) {
                Undo.RecordObject(composite, "Behaviour Tree (RemoveChild)");
                composite.children.Remove(child);
                EditorUtility.SetDirty(composite);
            }
        }
#endif
        #endregion Editor Compatibility
    }
}