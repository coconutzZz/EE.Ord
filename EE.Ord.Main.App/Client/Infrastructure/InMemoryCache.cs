using System;
using EE.Ord.Main.App.Client.Infrastructure.Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace EE.Ord.Main.App.Client.Infrastructure
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Clear()
        {
            _memoryCache.Dispose();
        }

        public bool Contains(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            _memoryCache.Set(key, value, absoluteExpiration);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _memoryCache.Set(key, value, slidingExpiration);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
