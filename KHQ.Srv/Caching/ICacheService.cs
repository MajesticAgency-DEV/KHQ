namespace KHQ.Srv.Caching
{
    public interface ICacheService
    {
        T GetOrCreate<T>(Func<T> createItem, int hours = 3, string customKey = null);
        Task<T> GetOrCreateAsync<T>(Func<Task<T>> createItem, int hours = 3, string customKey = null);
        void Remove<T>();
        void Remove(string key);
        void ClearAll();
    }

}
