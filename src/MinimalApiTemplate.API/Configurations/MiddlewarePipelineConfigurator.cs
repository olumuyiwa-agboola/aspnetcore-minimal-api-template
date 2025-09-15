namespace MinimalApiTemplate.API.Configurations
{
    public static class MiddlewarePipelineConfigurator
    {
        public static WebApplication ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.MapGet("/", () => "Hello World!");

            return app;
        }
    }
}
