namespace Vertem.News.Api.Configurations
{
    public static class CorsConfigurationExtensions
    {
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
        }

        public static void UseCorsConfig(this WebApplication app)
        {
            app.UseCors("corsapp");
        }
    }
}
