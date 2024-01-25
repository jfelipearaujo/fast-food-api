using Microsoft.AspNetCore.Http.HttpResults;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class ProductEndpoints
{
    private const string EndpointTag = "Product";

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var products = app.NewVersionedApi(EndpointTag);

        var group = products.MapGroup(ApiEndpoints.Products.BaseRoute)
            .HasApiVersion(ApiEndpoints.Products.V1.Version)
            .WithOpenApi();

        group.MapGet("{id}", GetProductById.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.GetById)
            .WithDescription("Search and return a product's data based on their identifier")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllProducts.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.GetAll)
            .WithDescription("Search and returns all products")
            .Produces<List<ProductEndpointResponse>>(StatusCodes.Status200OK);

        group.MapGet("/category/{category}", GetAllProductsByCategory.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.GetAllByCategory)
            .WithDescription("Searchs and returns all products based on their category")
            .Produces<List<ProductEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateProduct.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.Create)
            .WithDescription("Register a product")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("{id}/image", GetProductImage.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.GetImage)
            .WithDescription("Searchs and returns a product's image")
            .Produces<PhysicalFileHttpResult>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/image", UploadProductImage.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.UploadImage)
            .WithDescription("Upload an image for the product")
            .Produces(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPut("{id}", UpdateProduct.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.Update)
            .WithDescription("Update a product")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapDelete("{id}", DeleteProduct.HandleAsync)
            .WithName(ApiEndpoints.Products.V1.Delete)
            .WithDescription("Delete a product")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ApiError>(StatusCodes.Status404NotFound);
    }
}
