using UnityEngine;

namespace Aid.Data.Properties
{
    public class StringDataProperty : BaseDataProperty
    {
        public string Value { get; set; }

        internal StringDataProperty(string key, string defaultValue) : base(key)
        {
            Value = defaultValue;
            Load();
        }

        public override void Save()
        {
            PlayerPrefs.SetString(key, Value);
        }

        protected void Load()
        {
            if(PlayerPrefs.HasKey(key) == false) return;
            Value = PlayerPrefs.GetString(key);
        }
    }
}