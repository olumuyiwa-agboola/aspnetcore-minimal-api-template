namespace MinimalApiTemplate.API.Configurations
{
    public static class WebApplicationBuilderConfigurator
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureHostBuilder();
            builder.WebHost.ConfigureWebHostBuilder();
            builder.Services.ConfigureApplicationOptions();
            builder.Services.ConfigureLogger(builder.Configuration);

            return builder;
        }
    }
}