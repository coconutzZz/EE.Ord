using System;

namespace EE.Ord.Main.App.Client.Infrastructure.Abstractions.Cache
{
    public abstract class CacheManagerBase
    {
        private readonly ICache _cacheImpl;
        protected abstract string KeyPrefix { get; set; }

        #region ICache members
        protected CacheManagerBase(ICache cacheImpl)
        {
            _cacheImpl = cacheImpl;
        }

        public virtual void Clear()
        {
            _cacheImpl.Clear();
        }

        public virtual bool Contains(string key)
        {
            return _cacheImpl.Contains(AddPrefix(key));
        }

        public virtual T Get<T>(string key)
        {
            return _cacheImpl.Get<T>(AddPrefix(key));
        }

        public virtual void Set(string key, object value, DateTime absoluteExpiration)
        {
            _cacheImpl.Set(AddPrefix(key), value, absoluteExpiration);
        }

        public virtual void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _cacheImpl.Set(AddPrefix(key), value, slidingExpiration);
        }

        public virtual void Remove(string key)
        {
            _cacheImpl.Remove(AddPrefix(key));
        }
        #endregion

        public virtual T GetOrSet<T>(string key, Func<T> dataLoader, DateTime absoluteExpiration, string dataParameter = null) where T : class
        {
            key = ResolveKey(key, dataParameter);

            T result = Get<T>($"{key}");
            if (result != null)
                return result;

            result = dataLoader();
            Set(key, result, absoluteExpiration);

            return result;
        }

        public virtual T GetOrSet<T>(string key, Func<T> dataLoader, int cacheTimeInMinutes, string dataParameter = null) where T : class
        {
            return GetOrSet(key, dataLoader, SystemTime.Now().AddMinutes(cacheTimeInMinutes), dataParameter);
        }

        public virtual T GetOrSet<T>(string key, Func<T> dataLoader, TimeSpan cacheTime, string dataParameter = null) where T : class
        {
            return GetOrSet(key, dataLoader, SystemTime.Now() + cacheTime, dataParameter);
        }

        private string ResolveKey(string key, string dataParameter)
        {
            if (!string.IsNullOrWhiteSpace(dataParameter))
                key += "." + dataParameter;

            return key;
        }

        private string AddPrefix(string key)
        {
            return $"{KeyPrefix}.{key}";
        }
    }
}
