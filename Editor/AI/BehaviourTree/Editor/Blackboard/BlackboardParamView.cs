
using Aid.AI.BehaviourTree.Blackboard;
using UnityEditor.UIElements;
using UnityEngine;

namespace Aid.BehaviourTree.Editor
{
using UnityEditor;
using UnityEngine.UIElements;
    public class BlackboardParamView : VisualElement
    {
        private string PathToUiFile => $"{BehaviourTreeEditor.AssetsPath}/Editor/AI/BehaviourTree/Editor/Blackboard/UI/BlackboardParamView.uxml";

        private PropertyField propertyField;

        private TextField textField;

        private Button deleteButton;

        private BlackboardParam param;

        public System.Action<BlackboardParam> DeleteClicked;
        public BlackboardParamView(BlackboardParam param)
        {
            (EditorGUIUtility.Load(PathToUiFile) as VisualTreeAsset).CloneTree( this);

            this.param = param;
            textField = this.Q<TextField>();
            propertyField = this.Q<PropertyField>();
            deleteButton = this.Q<Button>("delete-button");

            deleteButton.clickable.clicked += Delete;

            textField.value = param.name;
            textField.RegisterValueChangedCallback(OnTextNameChanged);

            var serializedObj = new SerializedObject(param);
            propertyField.Bind(serializedObj);
            propertyField.bindingPath = "value";
            
            
            this.AddManipulator(new ContextualMenuManipulator( evt =>
            {
                evt.menu.AppendAction("Delete", Delete);
            }));
        }

        private void OnTextNameChanged(ChangeEvent<string> evt)
        {
            param.name = evt.newValue;
            AssetDatabase.SaveAssets();
        }

        public void EditName()
        {
            textField.Focus();
        }

        private void Delete(DropdownMenuAction dropdownMenuAction)
        {
            Delete();
        }

        private void Delete()
        {
            DeleteClicked?.Invoke(param);
            RemoveFromHierarchy();
        }
    }
}