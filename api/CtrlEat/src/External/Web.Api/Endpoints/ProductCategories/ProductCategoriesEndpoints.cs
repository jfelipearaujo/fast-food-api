using Web.Api.Extensions;

namespace Web.Api.Endpoints.ProductCategories;

public static class ProductCategoriesEndpoints
{
    private const string EndpointTag = "Product Category";

    public static void MapProductCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var productCategories = app.NewVersionedApi(EndpointTag);

        var group = productCategories.MapGroup(ApiEndpoints.ProductCategories.BaseRoute)
            .HasApiVersion(ApiEndpoints.ProductCategories.V1.Version)
            .WithOpenApi();

        group.MapGet("{id}", GetProductCategoryById.HandleAsync)
            .WithName(ApiEndpoints.ProductCategories.V1.GetById)
            .WithDescription("Searches and returns a product's category based on their identifier")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllProductCategories.HandleAsync)
            .WithName(ApiEndpoints.ProductCategories.V1.GetAll)
            .WithDescription("Searches and returns all registered product's categories")
            .Produces<List<ProductCategoryEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateProductCategory.HandleAsync)
            .WithName(ApiEndpoints.ProductCategories.V1.Create)
            .WithDescription("Register a new product category")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status201Created);

        group.MapPut("{id}", UpdateProductCategory.HandleAsync)
            .WithName(ApiEndpoints.ProductCategories.V1.Update)
            .WithDescription("Update product category")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapDelete("{id}", DeleteProductCategory.HandleAsync)
           .WithName(ApiEndpoints.ProductCategories.V1.Delete)
           .WithDescription("Delete a product category")
           .Produces(StatusCodes.Status204NoContent)
           .Produces<ApiError>(StatusCodes.Status404NotFound);
    }
}