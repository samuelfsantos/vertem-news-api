using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace Vertem.News.Api.Configurations
{
    public static class CacheConfigurationExtensions
    {
        public static void AddCacheConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            //services.AddDistributedMemoryCache();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Cache");
                options.InstanceName = $"CacheOficinaTech{env.EnvironmentName}";
            });

            //services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());

            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("redis:6379"));


        }
    }
}
