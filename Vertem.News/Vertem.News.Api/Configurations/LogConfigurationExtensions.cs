using Serilog;
using System.Reflection;

namespace Vertem.News.Api.Configurations
{
    public static class LogConfigurationExtensions
    {
        public static void AddLogConfig(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.WithProperty("Version", Assembly.GetEntryAssembly()!.GetName().Version)
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .Enrich.WithMemoryUsage()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5012/")
                .CreateLogger();
            builder.Logging.AddSerilog(logger);
        }
    }
}
