using Serilog;
using Serilog.Events;
using FluentValidation;
using Microsoft.Extensions.Options;
using MinimalApiTemplate.Core.Models.Config;

namespace MinimalApiTemplate.API.Configurations
{
    public static partial class WebApplicationBuilderConfigurator
    {
        public static WebApplicationBuilder ConfigureWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            builder.ConfigureCors();

            builder.ConfigureLogger();
            
            builder.ConfigureCaching();
            
            builder.ConfigureHelpers();
            
            builder.ConfigureHostBuilder();
            
            builder.ConfigureHttpClients();
            
            builder.ConfigureRepositories();
            
            builder.ConfigureWebHostBuilder();
            
            builder.ConfigureApiDocumentation();
            
            builder.ConfigureApplicationOptions();
            
            builder.ConfigureHttpContextAccessor();
            
            builder.ConfigureServicesAndHandlers();
            
            builder.ConfigureRoutingAndEndpoints();
            
            builder.ConfigureAuthenticationAndAuthorization();

            return builder;
        }

        public static void ConfigureApiDocumentation(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureAuthenticationAndAuthorization(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureCaching(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificMethods", builder =>
                {
                    builder.WithMethods("GET", "POST", "OPTIONS")
                    .AllowAnyOrigin()
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureHelpers(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureHostBuilder(this WebApplicationBuilder builder)
        {
            builder.Host.UseDefaultServiceProvider(options =>
            {
                options.ValidateOnBuild = true;
            });
        }

        public static void ConfigureHttpClients(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureHttpContextAccessor(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
        }

        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            FileLoggerSettings fileLoggingSettings = builder.Configuration.GetSection(nameof(FileLoggerSettings)).Get<FileLoggerSettings>()!;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    shared: true,
                    rollOnFileSizeLimit: true,
                    path: fileLoggingSettings.FilePath,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000, // 10 MB
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            builder.Services.AddSerilog();
        }

        public static void ConfigureRepositories(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureRoutingAndEndpoints(this WebApplicationBuilder builder)
        {
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
                options.LowercaseQueryStrings = true;
            });
        }

        public static void ConfigureServicesAndHandlers(this WebApplicationBuilder builder)
        {

        }

        public static void ConfigureWebHostBuilder(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);
        }

        public static void ConfigureApplicationOptions(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptionsWithFluentValidation<FileLoggerSettings, FileLoggerSettingsValidator>(nameof(FileLoggerSettings));
        }

        private static IServiceCollection AddOptionsWithFluentValidation<TOptions, TOptionsValidator>(this IServiceCollection services, string configurationSection) where TOptions : class, new() where TOptionsValidator : AbstractValidator<TOptions>
        {
            services.AddScoped<IValidator<TOptions>, TOptionsValidator>();

            services.AddOptions<TOptions>()
                .BindConfiguration(configurationSection)
                .ValidateOptionsWithFluentValidation()
                .ValidateOnStart();

            services.AddScoped(resolver => resolver.GetRequiredService<IOptionsSnapshot<TOptions>>().Value);

            return services;
        }

        private static OptionsBuilder<TOptions> ValidateOptionsWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
        {
            builder.Services.AddSingleton<IValidateOptions<TOptions>>(
                serviceProvider => new OptionsFluentValidationHandler<TOptions>(
                    serviceProvider,
                    builder.Name));

            return builder;
        }
    }

    file class OptionsFluentValidationHandler<TOptions>(IServiceProvider serviceProvider, string? name) : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly string? _name = name;

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            if (_name != null && _name != name)
            {
                return ValidateOptionsResult.Skip;
            }

            ArgumentNullException.ThrowIfNull(options, nameof(options));

            using var scope = serviceProvider.CreateScope();

            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

            var result = validator.Validate(options);

            if (result.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var type = options.GetType().Name;
            var errors = new List<string>();

            foreach (var error in result.Errors)
            {
                errors.Add($"Validation failed for {type}.{error.PropertyName}: with error: {error.ErrorMessage}");
            }

            return ValidateOptionsResult.Fail(errors);
        }
    }
}