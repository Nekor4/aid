using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Aid.Singletons
{
    /// <summary>
    /// The basic MonoBehaviour singleton implementation, this singleton is destroyed after scene changes, use <see cref="PersistentMonoSingleton{T}"/> if you want a persistent and global singleton instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        private static T _instance;
        private bool _isInitialized;

        public Action Initialized;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new()
                        {
                            name = Regex.Replace(typeof(T).Name, "(\\B[A-Z])", " $1")
                        };
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => _isInitialized;

        public static bool InstanceExists => _instance != null;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                InitializeSingleton();
            }
            else if (_instance != this)
            {
                // Destroy duplicates
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        protected virtual void OnInitializing()
        {
        }

        protected virtual void OnInitialized()
        {
            Initialized?.Invoke();
        }

        public virtual void InitializeSingleton()
        {
            if (_isInitialized)
            {
                return;
            }

            OnInitializing();
            _isInitialized = true;
            OnInitialized();
        }

        public virtual void ClearSingleton()
        {
            _isInitialized = false;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            _instance.ClearSingleton();
            if (Application.isPlaying)
            {
                Destroy(_instance.gameObject);
            }
            else
            {
                DestroyImmediate(_instance.gameObject);
            }
            _instance = null;
        }
    }
}