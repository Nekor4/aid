namespace Aid.Singletons
{
    public enum SingletonInitializationStatus
    {
        None,
        Initializing,
        Initialized
    }

    /// <summary>
    /// The singleton implementation for classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// The initialization status of the singleton's instance.
        /// </summary>
        private SingletonInitializationStatus _initializationStatus = SingletonInitializationStatus.None;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (Instance == null)
                {
                    //ensure that only one thread can execute
                    lock (typeof(T))
                    {
                        if (Instance == null)
                        {
                            _instance = new T();
                            _instance.InitializeSingleton();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => _initializationStatus == SingletonInitializationStatus.Initialized;

        #endregion

        #region Protected Methods

        protected virtual void OnInitializing()
        {

        }

        protected virtual void OnInitialized()
        {

        }

        #endregion

        #region Public Methods

        public virtual void InitializeSingleton()
        {
            if (_initializationStatus != SingletonInitializationStatus.None)
            {
                return;
            }

            _initializationStatus = SingletonInitializationStatus.Initializing;
            OnInitializing();
            _initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void ClearSingleton() { }

        public static void CreateInstance()
        {
            DestroyInstance();
            _instance = Instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            _instance.ClearSingleton();
            _instance = default;
        }

        #endregion
    }
}