using System;

namespace Patterns
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static bool IsInstanceCreated = false;
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(() =>
        {
            T _Instance = new T();
            _Instance.Initialize();
            IsInstanceCreated = true;
            return _Instance;
        });

        public static T Instance { get { return LazyInstance.Value; } }

        protected Singleton()
        {
            if (IsInstanceCreated)
            {
                throw new InvalidOperationException("Constructing a " + typeof(T).Name +
                    " manually is not allowed, use the Instance property.");
            }
        }

        public virtual void Initialize()
        {

        }
    }
}

