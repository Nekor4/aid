using UnityEngine;

namespace Aid.Data.Properties
{
    public class ComplexDataProperty<T> : BaseDataProperty
    {
        public T Value { get; set; }

        internal ComplexDataProperty(string key, T defaultValue) : base(key)
        {
            if (defaultValue.GetType().IsPrimitive)
            {
                throw new PrimitiveDataException();
            }
            
            Value = defaultValue;
            Load();
        }

        public override void Save()
        {
            var json = JsonUtility.ToJson(Value);
            PlayerPrefs.SetString(key, json);
        }

        protected void Load()
        {
            if(PlayerPrefs.HasKey(key) == false) return;
            var json = PlayerPrefs.GetString(key);
            Value = JsonUtility.FromJson<T>(json);
        }
    }
}