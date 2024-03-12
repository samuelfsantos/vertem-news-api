namespace Vertem.News.Api.Configurations
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Vertem - Desafio técnico",
                    Version = "v1",
                    Description = "API REST notícias.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Samuel Ferreira Santos",
                        Email = "samuel.santos.si@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/samuel-santos-0a243856/")
                    }
                });
                c.UseInlineDefinitionsForEnums();
            });
        }

        public static void UseSwaggerConfig(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
