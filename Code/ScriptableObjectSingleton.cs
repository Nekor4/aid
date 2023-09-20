using UnityEngine;

namespace Aid
{
	public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
	{
        private static T instance = null;
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log(typeof(T));
                    var objs = Resources.FindObjectsOfTypeAll<T>();
                    instance = objs[0];
                }
                
                return instance;
            }
        }
    }
}