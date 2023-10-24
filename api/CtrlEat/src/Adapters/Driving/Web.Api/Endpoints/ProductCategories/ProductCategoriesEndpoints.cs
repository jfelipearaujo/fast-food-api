using Domain.UseCases.ProductCategories.CreateProductCategory;
using Domain.UseCases.ProductCategories.CreateProductCategory.Request;
using Domain.UseCases.ProductCategories.DeleteProductCategory;
using Domain.UseCases.ProductCategories.DeleteProductCategory.Request;
using Domain.UseCases.ProductCategories.GetAllProductCategories;
using Domain.UseCases.ProductCategories.GetProductCategoryById;
using Domain.UseCases.ProductCategories.GetProductCategoryById.Request;
using Domain.UseCases.ProductCategories.UpdateProductCategory;
using Domain.UseCases.ProductCategories.UpdateProductCategory.Request;

using Web.Api.Endpoints.ProductCategories.Requests;
using Web.Api.Endpoints.ProductCategories.Responses;
using Web.Api.Endpoints.ProductCategories.Responses.Mapping;
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

        group.MapGet("{id}", GetProductCategoryById)
            .WithName(ApiEndpoints.ProductCategories.V1.GetById)
            .WithDescription("Searches and returns a product's category based on their identifier")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllProductCategories)
            .WithName(ApiEndpoints.ProductCategories.V1.GetAll)
            .WithDescription("Searches and returns all registered product's categories")
            .Produces<List<ProductCategoryEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateProductCategory)
            .WithName(ApiEndpoints.ProductCategories.V1.Create)
            .WithDescription("Register a new product category")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status201Created);

        group.MapPut("{id}", UpdateProductCategory)
            .WithName(ApiEndpoints.ProductCategories.V1.Update)
            .WithDescription("Update product category")
            .Produces<ProductCategoryEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapDelete("{id}", DeleteProductCategory)
           .WithName(ApiEndpoints.ProductCategories.V1.Delete)
           .WithDescription("Delete a product category")
           .Produces(StatusCodes.Status204NoContent)
           .Produces<ApiError>(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> GetProductCategoryById(
        Guid id,
        IGetProductCategoryByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var useCaseRequest = new GetProductCategoryByIdRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(useCaseRequest, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> GetAllProductCategories(
        IGetAllProductCategoriesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateProductCategory(
        CreateProductCategoryEndpointRequest endpointRequest,
        ICreateProductCategoryUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = new CreateProductCategoryRequest
        {
            Description = endpointRequest.Description,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        var response = result.Value.MapToResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.ProductCategories.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }

    public static async Task<IResult> UpdateProductCategory(
        Guid id,
        UpdateProductCategoryEndpointRequest endpointRequest,
        IUpdateProductCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductCategoryRequest
        {
            Id = id,
            Description = endpointRequest.Description,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.CreatedAtRoute(
            ApiEndpoints.ProductCategories.V1.GetById,
            response,
            new
            {
                id = response.Id,
            });
    }

    public static async Task<IResult> DeleteProductCategory(
        Guid id,
        IDeleteProductCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var useCaseRequest = new DeleteProductCategoryRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(useCaseRequest, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        return Results.NoContent();
    }
}