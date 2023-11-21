using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Aid.AI.BehaviourTree.Blackboard
{
#if UNITY_EDITOR
#endif

    public class Blackboard : ScriptableObject
    {
        public List<BlackboardParam> paramsList = new List<BlackboardParam>();

        public Blackboard Clone(string runnerName = "")
        {
            var clone = Instantiate(this);
            clone.name = $"{name}-{runnerName}";
            return clone;
        }

        public bool TryGetParam<T>(string paramName, out T param) where T : BlackboardParam
        {
            param = GetParam<T>(paramName);
            return param != null;
        }

        public T GetParam<T>(string paramName) where T : BlackboardParam
        {
            for (int i = 0; i < paramsList.Count; i++)
            {
                if (paramsList[i].name == paramName)
                {
                    var param = (T)paramsList[i];
                    if (param != null)
                        return param;
                }
            }

            return null;
        }

#if UNITY_EDITOR
        public BlackboardParam CreateParam(System.Type type)
        {
            var param = CreateInstance(type) as BlackboardParam;
            param.name = $"New {type.Name}";

            Undo.RecordObject(this, "Blackboard (CreateParam)");
            paramsList.Add(param);

            if (Application.isPlaying == false)
                AssetDatabase.AddObjectToAsset(param, this);

            Undo.RegisterCreatedObjectUndo(this, "Blackboard (CreateParam)");

            AssetDatabase.SaveAssets();
            return param;
        }

        public void DeleteParam(BlackboardParam param)
        {
            Undo.RecordObject(this, "Blackboard (DeleteParam)");
            paramsList.Remove(param);

            Undo.DestroyObjectImmediate(param);
            // AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}