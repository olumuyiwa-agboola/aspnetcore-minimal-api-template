namespace MinimalApiTemplate.API.Configurations
{
    public static class RequestPipelineConfigurator
    {
        public static WebApplication ConfigureRequestPipeline(this WebApplication app)
        {
            app.MapGet("/", () => "Hello World!");

            return app;
        }
    }
}
