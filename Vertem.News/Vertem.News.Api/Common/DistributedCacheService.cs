using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Vertem.News.Infra.Responses;
using Vertem.News.Infra.Base;


namespace Vertem.News.Api.Common
{
    public static class DistributedCacheService
    {
        //public static async Task<T?> GetCachedItemAsync<T>(this IDistributedCache cache, string key) where T : class
        //{
        //    var dataInBytes = await cache.GetAsync(key);

        //    if (dataInBytes is null)
        //    {
        //        return null;
        //    }

        //    var rawJson = System.Text.Encoding.UTF8.GetString(dataInBytes);

        //    return JsonSerializer.Deserialize<T>(rawJson);
        //}

        public static async Task<RequestResult<TResponse>?> GetCachedItemAsync<TResponse>(this IDistributedCache cache, string key) where TResponse : BaseOutput
        {
            var dataInBytes = await cache.GetAsync(key);

            if (dataInBytes is null)
            {
                return null;
            }

            var rawJson = System.Text.Encoding.UTF8.GetString(dataInBytes);

            return JsonSerializer.Deserialize<RequestResult<TResponse>?>(rawJson);
        }

        //public static async Task SaveItemAsync<T>(this IDistributedCache cache, T item, string key, int expirationInSeconds)
        //{
        //    var dataJson = JsonSerializer.Serialize(item);
        //    var dataInBytes = System.Text.Encoding.UTF8.GetBytes(dataJson);

        //    await cache.SetAsync(key, dataInBytes, new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(expirationInSeconds)
        //    });
        //}

        public static async Task SaveItemAsync<TResponse>(this IDistributedCache cache, RequestResult<TResponse> item, string key, int expirationInSeconds) where TResponse : BaseOutput
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
