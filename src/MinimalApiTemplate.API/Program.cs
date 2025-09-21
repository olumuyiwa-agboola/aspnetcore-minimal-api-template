using MinimalApiTemplate.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureWebApplicationBuilder()
            .Build();

app.ConfigureRequestPipeline()
    .Run();