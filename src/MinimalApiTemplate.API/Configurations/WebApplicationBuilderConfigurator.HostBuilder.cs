namespace MinimalApiTemplate.API.Configurations
{
    public static class HostBuilderConfigurator
    {
        public static IHostBuilder ConfigureHostBuilder(this IHostBuilder builder)
        {
            builder.UseDefaultServiceProvider(options =>
            {
                options.ValidateOnBuild = true;
            });

            return builder;
        }
    }
}