using Serilog;
using System.Reflection;
using Serilog.Exceptions;

namespace Vertem.News.Api.Configurations
{
    public static class LogConfigurationExtensions
    {
        public static void AddLogConfig(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Logging.ClearProviders();
            //var logger = new LoggerConfiguration()
            //    .MinimumLevel.Error()
            //    .Enrich.WithProperty("Version", Assembly.GetEntryAssembly()!.GetName().Version)
            //    .Enrich.WithEnvironmentName()
            //    .Enrich.WithMachineName()
            //    .Enrich.WithProcessId()
            //    .Enrich.WithThreadId()
            //    .Enrich.WithMemoryUsage()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console()
            //    .WriteTo.Seq("http://localhost:5012/")
            //    .CreateLogger();
            //builder.Logging.AddSerilog(logger);

            builder.Host.UseSerilog((context, loggerConfig) => {
                loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .WriteTo.Seq("vertem-news-seq:5012");
            });
        }

        //public static void UseLogConfig(this WebApplication app)
        //{
        //    app.UseSerilogRequestLogging(options =>
        //    {
        //        options.EnrichDiagnosticContext = Enricher.HttpRequestEnricher;
        //    });
        //}

    }
}
