using System;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public interface IAsanaCache
    {
        void SetRegion(string name);
        void Add(string key, object value);
        void Add(string key, object value, TimeSpan timeout);
        void Set(string key, object value);
        void Set(string key, object value, TimeSpan timeout);
        void SetFromTask(string key, Task<object> task, TimeSpan timeout);
        void SetFromTask(string key, Task<object> task);
        void AddBannedType(Type t);
        bool Contains(string key);
        void Flush();
        object Get(string key);
        void Remove(string key);
        List<T> GetAllOfType<T>(string keyStartsWith = null);
        List<object> GetAllNotStartingWith(string keyDoesntStartWith);
    }
    public class MemCache : IAsanaCache
    {
        private readonly static MemoryCache MemoryCache = MemoryCache.Default;
        public string Prefix = String.Empty;

        public MemCache(string prefix = null)
        {
            if (prefix != null)
                Prefix = prefix;
        }

        #region ICache Members
        public void SetRegion(string name)
        {
            Prefix = name;
        }

        public List<T> GetAllOfType<T>(string keyStartsWith = null)
        {
            var objectsOfT = new List<T>();

            if (keyStartsWith != null)
                foreach (var entry in MemoryCache.Where(entry => entry.Key.StartsWith(Prefix + keyStartsWith)))
                {
                    if(entry.Value.GetType() == typeof(T))
                        objectsOfT.Add((T) entry.Value);
                }
            else
                foreach (var entry in MemoryCache.Where(entry => entry.Key.StartsWith(Prefix)))
                {
                    if (entry.Value.GetType() == typeof(T))
                        objectsOfT.Add((T) entry.Value);
                }
            return objectsOfT;
        }
        public List<object> GetAllNotStartingWith(string keyDoesntStartWith)
        {
            var objectsOfT = new List<object>();

            foreach (var entry in MemoryCache.Where(entry => !entry.Key.StartsWith(Prefix + keyDoesntStartWith)))
            {
                objectsOfT.Add(entry.Value);
            }
            return objectsOfT;
        }

        public string PrefixedKey(string key)
        {
            return Prefix + key;
        }

        public object Get(string key)
        {
            if (MemoryCache.Contains(PrefixedKey(key)))
                return MemoryCache.GetCacheItem(PrefixedKey(key)).Value;
            return null;
        }
        public void Put(string key, object item)
        {
            if (MemoryCache.Contains(PrefixedKey(key)))
                MemoryCache[PrefixedKey(key)] = item;
            else Add(key, item);
        }
        public void Add(string key, object value, TimeSpan timeout)
        {
            if (!BannedTypes.Contains(value.GetType()))
            {
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(timeout)
                };
                MemoryCache.Add(PrefixedKey(key), value, policy);
            }
        }
        public void Add(string key, object value)
        {
            if (!BannedTypes.Contains(value.GetType()))
            {
                MemoryCache.Add(PrefixedKey(key), value, new CacheItemPolicy());
            }
        }

        public List<Type> BannedTypes = new List<Type>();
        public void AddBannedType(Type t)
        {
            BannedTypes.Add(t);
        }

        public bool Contains(string key)
        {
            return MemoryCache.Contains(PrefixedKey(key));
        }
        public void Flush()
        {
            foreach (var entry in MemoryCache)
            {
                if(entry.Key.StartsWith(Prefix))
                    MemoryCache.Remove(entry.Key);
            }
        }
        public void Remove(string key)
        {
            MemoryCache.Remove(PrefixedKey(key));
        }
        public void Set(string key, object value, TimeSpan timeout)
        {
            if (MemoryCache.Contains(PrefixedKey(key)))
                MemoryCache.Remove(PrefixedKey(key));
            Add(key, value, timeout);
        }

        public void Set(string key, object value)
        {
            if (MemoryCache.Contains(PrefixedKey(key)))
                MemoryCache.Remove(PrefixedKey(key));
            Add(key, value);
        }
        public async void SetFromTask(string key, Task<object> task, TimeSpan timeout)
        {
            await task.ContinueWith(async (value) =>
            {
                if (MemoryCache.Contains(PrefixedKey(key)))
                    MemoryCache.Remove(PrefixedKey(key));
                Add(key, await value, timeout);
            });
        }
        public async void SetFromTask(string key, Task<object> task)
        {
            await task.ContinueWith(async (value) =>
            {
                if (MemoryCache.Contains(PrefixedKey(key)))
                    MemoryCache.Remove(PrefixedKey(key));
                Add(key, await value);
            });
        }

        #endregion
    }
}