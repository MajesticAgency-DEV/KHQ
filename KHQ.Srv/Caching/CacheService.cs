using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace KHQ.Srv.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private static readonly ConcurrentBag<string> _keys = new();

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        private string GetKey<T>(string customKey = null) => customKey ?? $"{typeof(T).Name}_All";

        public T GetOrCreate<T>(Func<T> createItem, int hours = 3, string customKey = null)
        {
            string key = GetKey<T>(customKey);

            if (!_keys.Contains(key))
                _keys.Add(key);

            if (!_cache.TryGetValue(key, out T cacheEntry))
            {
                cacheEntry = createItem();

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(hours),
                    Priority = CacheItemPriority.High
                };

                _cache.Set(key, cacheEntry, options);
            }

            return cacheEntry;
        }

        public async Task<T> GetOrCreateAsync<T>(Func<Task<T>> createItem, int hours = 3, string customKey = null)
        {
            string key = GetKey<T>(customKey);

            if (!_keys.Contains(key))
                _keys.Add(key);

            if (!_cache.TryGetValue(key, out T cacheEntry))
            {
                cacheEntry = await createItem();

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(hours),
                    Priority = CacheItemPriority.High
                };

                _cache.Set(key, cacheEntry, options);
            }

            return cacheEntry;
        }

        public void Remove<T>()
        {
            string key = GetKey<T>();
            _cache.Remove(key);
            _keys.TryTake(out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.TryTake(out _);
        }

        public void ClearAll()
        {
            foreach (var key in _keys)
                _cache.Remove(key);

            while (_keys.TryTake(out _)) { }
        }
    }
}
