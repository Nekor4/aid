using System.Collections.Generic;
using Aid.Data.Properties;

namespace Aid.Data
{
    public static class DataStorage
    {
        private static List<BaseDataProperty> _list;

        public static BoolDataProperty GetProperty(string key, bool defaultValue)
        {
            if (TryFindProperty(key, out var property))
                return property as BoolDataProperty;

            var prop = new BoolDataProperty(key, defaultValue);
            _list.Add(prop);
            return prop;
        }
        
        public static IntDataProperty GetProperty(string key, int defaultValue)
        {
            if (TryFindProperty(key, out var property))
                return property as IntDataProperty;

            var prop = new IntDataProperty(key, defaultValue);
            _list.Add(prop);
            return prop;
        }
        
        public static StringDataProperty GetProperty(string key, string defaultValue)
        {
            if (TryFindProperty(key, out var property))
                return property as StringDataProperty;

            var prop = new StringDataProperty(key, defaultValue);
            _list.Add(prop);
            return prop;
        }
        
        public static FloatDataProperty GetProperty(string key, float defaultValue)
        {
            if (TryFindProperty(key, out var property))
                return property as FloatDataProperty;

            var prop = new FloatDataProperty(key, defaultValue);
            _list.Add(prop);
            return prop;
        }
        
        public static ComplexDataProperty<T> GetProperty<T>(string key, T defaultValue)
        {
            if (TryFindProperty(key, out var property))
                return property as ComplexDataProperty<T>;

            var prop = new ComplexDataProperty<T>(key, defaultValue);
            _list.Add(prop);
            return prop;
        }
        
        private static bool TryFindProperty(string key, out BaseDataProperty property)
        {
            property = null;

            _list ??= new List<BaseDataProperty>(16);

            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].key == key)
                {
                    property = _list[i];
                    return true;
                }
            }

            return false;
        }

        public static void Save()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Save();
            }
        }
    }
}