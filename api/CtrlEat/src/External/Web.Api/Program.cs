using Application;

using HealthChecks.UI.Client;

using Infrastructure;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using MinimalHelpers.OpenApi;

using Persistence;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Text.Json.Serialization;

using Web.Api.Endpoints.Clients;
using Web.Api.Endpoints.Orders;
using Web.Api.Endpoints.ProductCategories;
using Web.Api.Endpoints.Products;
using Web.Api.Extensions;
using Web.Api.Middlewares;
using Web.Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");

builder.Services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddConsole();
});

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;

}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
})
.EnableApiVersionBinding();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();
    options.AddMissingSchemas();
});

builder.Services.AddPersistence();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapProductCategoryEndpoints();
app.MapProductEndpoints();
app.MapClientEndpoints();
app.MapOrderEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });

    app.UseReDoc(c =>
    {
        c.RoutePrefix = "docs";
        c.DocumentTitle = "Ctrl+Eat API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }