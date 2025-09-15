using Serilog;
using Serilog.Events;
using MinimalApiTemplate.Core.Models.Config;

namespace MinimalApiTemplate.API.Configurations
{
    public static class LoggerConfigurator
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerSettings = configuration.GetSection(nameof(LoggerSettings)).Get<LoggerSettings>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    shared: true,
                    rollOnFileSizeLimit: true,
                    path: loggerSettings!.FilePath!,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000, // 10 MB
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            services.AddSerilog();

            return services;
        }
    }
}