using UnityEngine;

namespace Aid.Data.Properties
{
    public class FloatDataProperty: BaseDataProperty
    {
        public float Value { get; set; }

        internal FloatDataProperty(string key, float defaultValue = 0f) : base(key)
        {
            Value = defaultValue;
            Load();
        }

        public override void Save()
        {
            PlayerPrefs.SetFloat(key, Value);
        }

        protected void Load()
        {
            if(PlayerPrefs.HasKey(key) == false) return;
            Value = PlayerPrefs.GetFloat(key);
        }
    }
}