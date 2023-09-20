namespace Aid.Data.Properties
{
    public abstract class BaseDataProperty 
    {
        public readonly string key;

        internal BaseDataProperty(string key)
        {
            this.key = key;
        }

        public abstract void Save();
    }
}