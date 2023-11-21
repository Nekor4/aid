using Aid.AI.BehaviourTree;
using UnityEditor.Callbacks;

namespace Aid.BehaviourTree.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class BehaviourTreeEditor : EditorWindow
    {
        public const string AssetsPath = "Assets/External/Aid";
        
        private BehaviourTreeView treeView;
        private InspectorView inspetorView;
        private BlackboardView blackboardView;
        private TextElement treeName;
        
        private AI.BehaviourTree.BehaviourTree currentTree;


        [MenuItem("Aid/AI/Behaviour Tree Viewer")]
        public static void OpenWindow()
        {
            var window = GetWindow<BehaviourTreeEditor>();
            window.Repaint();
            window.titleContent = new GUIContent("BehaviourTreeEditor");
            window.Show();
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is AI.BehaviourTree.BehaviourTree)
            {
                OpenWindow();
                return true;
            }

            return false;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;


            // Import UXML
             var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/Editor/AI/BehaviourTree/Editor/UI/BehaviourTreeEditor.uxml");

             if (visualTree == null)
             {
                 Debug.LogError("Could not load UXML file for BehaviourTreeEditor.uxml at path: " +
                                $"{AssetsPath}/Editor/AI/BehaviourTree/Editor/UI/BehaviourTreeEditor.uxml");
                 return;
             }
             
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>($"{AssetsPath}/Editor/AI/BehaviourTree/Editor/UI/BehaviourTreeEditor.uss");

            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviourTreeView>();
            inspetorView = root.Q<InspectorView>();
            blackboardView = root.Q<BlackboardView>();
            treeName = root.Q<TextElement>("tree-name");

            treeView.Selected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }


        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            switch (obj) 
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                
                case PlayModeStateChange.ExitingEditMode:
                    break;
                
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }


        private void OnSelectionChange()
        {
            if(treeView == null) return;

            var tree = Selection.activeObject as AI.BehaviourTree.BehaviourTree;
            
            if(Selection.activeGameObject!= null)
                if (Selection.activeGameObject.TryGetComponent<BehaviourTreeRunner>(out var runner))
                {
                    tree = runner.RunningTree;
                }

            if (tree == null && currentTree == null)
            {
                treeView.DrawEmpty();
                treeName.text = "No Tree Selected";
                blackboardView.DrawEmpty();

                return;
            }

            if (Application.isPlaying)
            {
                if (tree != null)
                {
                    treeView.PopulateView(tree);
                    blackboardView.PopulateView(tree.Blackboard);
                    treeName.text = tree.name;
                }
            }
            else if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
                blackboardView.PopulateView(tree.Blackboard);
                treeName.text = tree.name;
            }

            if (tree != null)
            {
                currentTree = tree;
            }
        }

        private void OnNodeSelectionChanged(NodeView nodeView)
        {
            inspetorView.UpdateSelection(nodeView);
        }

        private void OnInspectorUpdate()
        {
            treeView?.UpdateNodeState();
        }
    }
}