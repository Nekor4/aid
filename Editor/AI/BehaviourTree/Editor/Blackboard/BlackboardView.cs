using UnityEngine;

namespace Aid.BehaviourTree.Editor
{
    using System;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    public class BlackboardView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BlackboardView, VisualElement.UxmlTraits>
        {
        }

        private ToolbarMenu toolbarMenu;
        private ListView listView;
        private Blackboard currrentBlackboard;

        public BlackboardView()
        {
            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            PopulateView(currrentBlackboard);
            AssetDatabase.SaveAssets();
        }

        public void DrawEmpty()
        {
            listView = this.Q<ListView>();

            while (listView.hierarchy.childCount > 0)
                listView.hierarchy.RemoveAt(0);
        }

        public void PopulateView(Blackboard blackboard)
        {
            currrentBlackboard = blackboard;

            ValidateBlackboard();
            listView = this.Q<ListView>();

            while (listView.hierarchy.childCount > 0)
                listView.hierarchy.RemoveAt(0);

            blackboard.paramsList.ForEach(CreateParamView);

            DrawToolbar();
        }

        private void ValidateBlackboard()
        {
            if (currrentBlackboard == null) return;
            for (int i = 0; i < currrentBlackboard.paramsList.Count; i++)
            {
                if (currrentBlackboard.paramsList[i] == null)
                {
                    currrentBlackboard.paramsList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void DrawToolbar()
        {
            toolbarMenu = this.Q<ToolbarMenu>();
            if (toolbarMenu == null) return;

            var types = TypeCache.GetTypesDerivedFrom<BlackboardParam>();
            foreach (var type in types)
            {
                toolbarMenu.menu.AppendAction(type.Name, (a) => { CreateParam(type); });
            }
        }

        private void CreateParam(Type type)
        {
            var param = currrentBlackboard.CreateParam(type);
            CreateParamView(param, true);
        }

        private void CreateParamView(BlackboardParam param)
        {
            CreateParamView(param, false);
        }

        private void CreateParamView(BlackboardParam param, bool focus)
        {
            var paramView = new BlackboardParamView(param);
            paramView.DeleteClicked = OnDeleteClicked;
            listView.hierarchy.Add(paramView);
            if (focus)
                paramView.EditName();
        }

        private void OnDeleteClicked(BlackboardParam param)
        {
            currrentBlackboard.DeleteParam(param);
        }
    }
}