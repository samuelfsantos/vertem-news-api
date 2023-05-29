using Microsoft.Extensions.Options;
using Vertem.News.Services.Configurations;

namespace Vertem.News.Api.Configurations
{
    public static class NewsApiOrgServiceConfigurationExtensions
    {
        public static void AddNewsApiOrgServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var _configurations = new NewsApiOrgServiceConfiguration();
            new ConfigureFromConfigurationOptions<NewsApiOrgServiceConfiguration>(configuration.GetSection("NewsApiOrgServiceConfiguration")).Configure(_configurations);

            services.AddSingleton(_configurations);
        }
    }
}
