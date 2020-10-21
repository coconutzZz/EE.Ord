using System;

namespace EE.Ord.Main.App.Client.Infrastructure.Abstractions.Cache
{
    public interface ICache
    {
        void Clear();
        bool Contains(string key);
        T Get<T>(string key);
        void Set(string key, object value, DateTime absoluteExpiration);
        void Set(string key, object value, TimeSpan slidingExpiration);
        void Remove(string key);
    }
}
