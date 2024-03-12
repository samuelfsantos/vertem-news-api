using MediatR;

namespace Vertem.News.Api.Configurations
{
    public static class MediatrConfigurationExtensions
    {
        public static void AddMediatrConfig(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Vertem.News.Application.AssemblyReference).Assembly);
            services.AddMediatR(typeof(Vertem.News.Domain.AssemblyReference).Assembly);
        }
    }
}
