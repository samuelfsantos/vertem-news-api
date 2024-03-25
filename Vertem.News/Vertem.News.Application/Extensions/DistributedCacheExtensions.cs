using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Vertem.News.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T?> GetCacheAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            var dataInBytes = await cache.GetAsync(key);

            if (dataInBytes is null)
            {
                return null;
            }

            var rawJson = System.Text.Encoding.UTF8.GetString(dataInBytes);

            return JsonSerializer.Deserialize<T>(rawJson);
        }

        public static async Task SaveCacheAsync<T>(this IDistributedCache cache, T item, string key, int expirationInSeconds)
        {
            var dataJson = JsonSerializer.Serialize(item);
            var dataInBytes = System.Text.Encoding.UTF8.GetBytes(dataJson);

            await cache.SetAsync(key, dataInBytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(expirationInSeconds)
            });
        }
    }
}