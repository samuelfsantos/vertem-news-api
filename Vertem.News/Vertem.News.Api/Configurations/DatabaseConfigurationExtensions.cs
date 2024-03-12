using Microsoft.EntityFrameworkCore;
using Vertem.News.Infra.Data.Contexts;

namespace Vertem.News.Api.Configurations
{
    public static class DatabaseConfigurationExtensions
    {
        public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Database"));
            });
        }
    }
}
