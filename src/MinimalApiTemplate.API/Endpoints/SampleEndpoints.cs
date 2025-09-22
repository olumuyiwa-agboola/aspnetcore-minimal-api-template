namespace MinimalApiTemplate.API.Endpoints
{
    public static class SampleEndpoints
    {
        public static IEndpointRouteBuilder MapSampleEndpoints(this IEndpointRouteBuilder app) 
        {
            var sample = app.MapGroup("/sample")
                            .WithTags("Sample");

            sample.MapGet("/", () => "Sample endpoint is working!")
                    .WithName("GetSampleEndpointStatus")
                    .WithSummary("Check the status of the sample endpoint")
                    .WithDescription("This endpoint returns a simple message indicating that the sample endpoint is operational.")
                    .Produces<string>(StatusCodes.Status200OK);

            return app;
        }
    }
}
