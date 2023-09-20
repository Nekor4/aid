using UnityEngine;

namespace Aid.Data.Properties
{
    public class BoolDataProperty : BaseDataProperty
    {
        public bool Value { get; set; }

        internal BoolDataProperty(string key, bool defaultValue) : base(key)
        {
            Value = defaultValue;
            Load();
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(key, Value ? 1 : 0);
        }

        protected void Load()
        {
            if(PlayerPrefs.HasKey(key) == false) return;
            Value = PlayerPrefs.GetInt(key) == 1;
        }
    }
}