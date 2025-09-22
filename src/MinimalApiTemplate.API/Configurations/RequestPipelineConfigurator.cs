using MinimalApiTemplate.API.Endpoints;

namespace MinimalApiTemplate.API.Configurations
{
    public static class RequestPipelineConfigurator
    {
        public static WebApplication ConfigureRequestPipeline(this WebApplication app)
        {
            app.MapGet("/", () => "Hello World!");

            app.MapSampleEndpoints();

            return app;
        }
    }
}
