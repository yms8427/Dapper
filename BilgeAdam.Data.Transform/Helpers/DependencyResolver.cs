using System;
using System.Collections.Concurrent;

namespace BilgeAdam.Data.Transform.Helpers
{
    public class DependencyResolver
    {
        private static ConcurrentDictionary<string, object> registry;
        static DependencyResolver()
        {
            registry = new ConcurrentDictionary<string, object>();
        }

        public T Get<T>()
        {
            var key = typeof(T).FullName;
            if (registry.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            throw new Exception($"No type found that is registered as {typeof(T).Name}");
        }

        public void Register<TInterface, TClass>() where TClass : class
        {
            var key = typeof(TInterface).FullName;
            var type = Activator.CreateInstance<TClass>();
            registry.TryAdd(key, type);
        }
    }
}
