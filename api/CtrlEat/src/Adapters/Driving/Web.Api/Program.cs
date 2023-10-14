using Application;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;

using MinimalHelpers.OpenApi;

using Persistence;

using System.Text.Json.Serialization;

using Web.Api.Endpoints.Clients;
using Web.Api.Endpoints.Orders;
using Web.Api.Endpoints.ProductCategories;
using Web.Api.Endpoints.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddMissingSchemas();
});

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProductCategoryEndpoints();
app.MapProductEndpoints();
app.MapClientEndpoints();
app.MapOrderEndpoints();

app.Run();