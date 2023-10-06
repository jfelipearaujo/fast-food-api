using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.ProductCategories.Mapping;
using Web.Api.Endpoints.ProductCategories.Requests;
using Web.Api.Endpoints.ProductCategories.Responses;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.ProductCategories;

public static class ProductCategoriesEndpoints
{
    private const string EndpointTag = "Product Category";

    public static void MapProductCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products/categories")
            .WithTags(EndpointTag);

        group.MapGet("{id}", GetProductCategoryById)
            .WithName(nameof(GetProductCategoryById))
            .WithOpenApi();

        group.MapGet("/", GetAllProductCategories)
            .WithName(nameof(GetAllProductCategories))
            .WithOpenApi();

        group.MapPost("/", CreateProductCategory)
            .WithName(nameof(CreateProductCategory))
            .WithOpenApi();

        group.MapPut("{id}", UpdateProductCategory)
            .WithName(nameof(UpdateProductCategory))
            .WithOpenApi();

        group.MapDelete("{id}", DeleteProductCategory)
           .WithName(nameof(DeleteProductCategory))
           .WithOpenApi();
    }

    public static async Task<Results<Ok<ProductCategoryEndpointResponse>, NotFound<ApiError>>> GetProductCategoryById(
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
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<List<ProductCategoryEndpointResponse>>> GetAllProductCategories(
        IGetAllProductCategoriesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<CreatedAtRoute<ProductCategoryEndpointResponse>> CreateProductCategory(
        CreateProductCategoryEndpointRequest endpointRequest,
        ICreateProductCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new CreateProductCategoryRequest
        {
            Description = endpointRequest.Description,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetProductCategoryById),
            new
            {
                id = response.Id,
            });
    }

    public static async Task<Results<CreatedAtRoute<ProductCategoryEndpointResponse>, NotFound<ApiError>>> UpdateProductCategory(
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
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetProductCategoryById),
            new
            {
                id = response.Id,
            });
    }

    public static async Task<Results<NoContent, NotFound<ApiError>>> DeleteProductCategory(
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
            return TypedResults.NotFound(result.ToApiError());
        }

        return TypedResults.NoContent();
    }
}