namespace MinimalApiTemplate.API.Configurations
{
    public static class WebHostBuilderConfigurator
    {
        public static IWebHostBuilder ConfigureWebHostBuilder(this IWebHostBuilder builder)
        {
            builder.ConfigureKestrel(options => options.AddServerHeader = false);

            return builder;
        }
    }
}
