using UnityEngine;

namespace Aid.Data.Properties
{
    public class IntDataProperty : BaseDataProperty
    {
        public int Value { get; set; }

        internal IntDataProperty(string key, int defaultValue) : base(key)
        {
            Value = defaultValue;
            Load();
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(key, Value);
        }

        protected void Load()
        {
            if(PlayerPrefs.HasKey(key) == false) return;
            Value = PlayerPrefs.GetInt(key);
        }
    }
}