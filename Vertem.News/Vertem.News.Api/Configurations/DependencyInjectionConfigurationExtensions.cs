using Confluent.Kafka;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Data;
using Vertem.News.Infra.Data.Repositories;
using Vertem.News.Infra.PipelineBehavior;
using Vertem.News.Services.NewsApiOrg;

namespace Vertem.News.Api.Configurations
{
    public static class DependencyInjectionConfigurationExtensions
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INoticiaRepository, NoticiaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsApiOrgService, NewsApiOrgService>();

            services.Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(@class => @class.AssignableTo(typeof(IShallowValidator<>))).AsImplementedInterfaces())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ShallowValidationBehavior<,>));

            services.Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(@class => @class.AssignableTo(typeof(IDeepValidator<>))).AsImplementedInterfaces())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DeepValidationBehavior<,>));

            services.AddSingleton(new ProducerBuilder<string, string>(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("bootstrap.servers", configuration.GetConnectionString("Kafka"))
            }));
        }
    }
}
