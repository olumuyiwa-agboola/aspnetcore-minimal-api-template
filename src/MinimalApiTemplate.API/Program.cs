using MinimalApiTemplate.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureBuilder()
            .Build();

app.ConfigureMiddlewarePipeline()
    .Run();