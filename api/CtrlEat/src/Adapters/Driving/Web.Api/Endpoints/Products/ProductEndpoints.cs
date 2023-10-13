using Domain.UseCases.Products.CreateProduct;
using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.DeleteProduct.Requests;
using Domain.UseCases.Products.GetAllProducts;
using Domain.UseCases.Products.GetProductById;
using Domain.UseCases.Products.GetProductById.Requests;
using Domain.UseCases.Products.GetProductsByCategory;
using Domain.UseCases.Products.GetProductsByCategory.Requests;
using Domain.UseCases.Products.UpdateProduct;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.Products.Requests;
using Web.Api.Endpoints.Products.Requests.Mapping;
using Web.Api.Endpoints.Products.Responses;
using Web.Api.Endpoints.Products.Responses.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class ProductEndpoints
{
    private const string EndpointTag = "Product";

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products")
            .WithTags(EndpointTag);

        group.MapGet("{id}", GetProductById)
            .WithName(nameof(GetProductById))
            .WithOpenApi();

        group.MapGet("/", GetAllProducts)
            .WithName(nameof(GetAllProducts))
            .WithOpenApi();

        group.MapGet("/category/{category}", GetAllProductsByCategory)
            .WithName(nameof(GetAllProductsByCategory))
            .WithOpenApi();

        group.MapPost("/", CreateProduct)
            .WithName(nameof(CreateProduct))
            .WithOpenApi();

        group.MapGet("{id}/image", GetProductImage)
            .WithName(nameof(GetProductImage))
            .WithOpenApi();

        group.MapPost("{id}/image", UploadProductImage)
            .WithName(nameof(UploadProductImage))
            .WithOpenApi();

        group.MapPut("{id}", UpdateProduct)
            .WithName(nameof(UpdateProduct))
            .WithOpenApi();

        group.MapDelete("{id}", DeleteProduct)
            .WithName(nameof(DeleteProduct))
            .WithOpenApi();
    }

    public static async Task<Results<Ok<ProductEndpointResponse>, NotFound<ApiError>>> GetProductById(
        Guid id,
        IGetProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<List<ProductEndpointResponse>>> GetAllProducts(
        IGetAllProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<List<ProductEndpointResponse>>> GetAllProductsByCategory(
        string category,
        IGetProductsByCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductsByCategoryRequest
        {
            Category = category,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Results<CreatedAtRoute<ProductEndpointResponse>, NotFound<ApiError>>> CreateProduct(
        CreateProductEndpointRequest endpointRequest,
        ICreateProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetProductById),
            new
            {
                id = response.Id
            });
    }

    public static async Task<Ok> GetProductImage(
        Guid id,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var dir = Path.Join(Directory.GetCurrentDirectory(), "images");
        var extension = Path.GetExtension(file.FileName);
        var tempFile = Path.Join(dir, $"{id}{extension}");

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using var stream = File.OpenWrite(tempFile);
        await file.CopyToAsync(stream, cancellationToken);

        return TypedResults.Ok();
    }

    public static async Task<Ok> UploadProductImage(
        Guid id,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var dir = Path.Join(Directory.GetCurrentDirectory(), "images");
        var extension = Path.GetExtension(file.FileName);
        var tempFile = Path.Join(dir, $"{id}{extension}");

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using var stream = File.OpenWrite(tempFile);
        await file.CopyToAsync(stream, cancellationToken);

        return TypedResults.Ok();
    }

    public static async Task<Results<CreatedAtRoute<ProductEndpointResponse>, NotFound<ApiError>>> UpdateProduct(
        Guid id,
        UpdateProductEndpointRequest endpointRequest,
        IUpdateProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetProductById),
            new
            {
                id = response.Id
            });
    }

    public static async Task<Results<NoContent, NotFound<ApiError>>> DeleteProduct(
        Guid id,
        IDeleteProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest
        {
            Id = id,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        return TypedResults.NoContent();
    }
}
