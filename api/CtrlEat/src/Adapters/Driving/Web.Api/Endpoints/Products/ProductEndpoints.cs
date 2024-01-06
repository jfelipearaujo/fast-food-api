using Domain.UseCases.Products.CreateProduct;
using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.DeleteProduct.Requests;
using Domain.UseCases.Products.GetAllProducts;
using Domain.UseCases.Products.GetProductById;
using Domain.UseCases.Products.GetProductById.Requests;
using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductImage.Requests;
using Domain.UseCases.Products.GetProductsByCategory;
using Domain.UseCases.Products.GetProductsByCategory.Requests;
using Domain.UseCases.Products.UpdateProduct;
using Domain.UseCases.Products.UploadProductImage;
using Domain.UseCases.Products.UploadProductImage.Requests;

using Microsoft.AspNetCore.Http.HttpResults;
using Web.Api.Common.Constants;
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
        var products = app.NewVersionedApi(EndpointTag);

        var group = products.MapGroup(ApiEndpoints.Products.BaseRoute)
            .HasApiVersion(ApiEndpoints.Products.V1.Version)
            .WithOpenApi();

        group.MapGet("{id}", GetProductById)
            .WithName(ApiEndpoints.Products.V1.GetById)
            .WithDescription("Search and return a product's data based on their identifier")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllProducts)
            .WithName(ApiEndpoints.Products.V1.GetAll)
            .WithDescription("Search and returns all products")
            .Produces<List<ProductEndpointResponse>>(StatusCodes.Status200OK);

        group.MapGet("/category/{category}", GetAllProductsByCategory)
            .WithName(ApiEndpoints.Products.V1.GetAllByCategory)
            .WithDescription("Searchs and returns all products based on their category")
            .Produces<List<ProductEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateProduct)
            .WithName(ApiEndpoints.Products.V1.Create)
            .WithDescription("Register a product")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("{id}/image", GetProductImage)
            .WithName(ApiEndpoints.Products.V1.GetImage)
            .WithDescription("Searchs and returns a product's image")
            .Produces<PhysicalFileHttpResult>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/image", UploadProductImage)
            .WithName(ApiEndpoints.Products.V1.UploadImage)
            .WithDescription("Upload an image for the product")
            .Produces(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPut("{id}", UpdateProduct)
            .WithName(ApiEndpoints.Products.V1.Update)
            .WithDescription("Update a product")
            .Produces<ProductEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapDelete("{id}", DeleteProduct)
            .WithName(ApiEndpoints.Products.V1.Delete)
            .WithDescription("Delete a product")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ApiError>(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> GetProductById(
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
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> GetAllProducts(
        IGetAllProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> GetAllProductsByCategory(
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

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateProduct(
        CreateProductEndpointRequest endpointRequest,
        ICreateProductUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.Products.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }

    public static async Task<IResult> GetProductImage(
        Guid id,
        IGetProductImageUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductImageRequest
        {
            Id = id,
        };

        var result = await useCase.Execute(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.File(result.Value, ContentTypes.ImageJpeg);
    }

    public static async Task<IResult> UploadProductImage(
        Guid id,
        IFormFile file,
        IUploadProductImageUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var fileUrl = linkGenerator.GetUriByRouteValues(
            httpContext,
            nameof(GetProductImage),
            new
            {
                id
            });

        var request = new UploadProductImageRequest
        {
            Id = id,
            File = file,
            ImageUrl = fileUrl,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.CreatedAtRoute(
            ApiEndpoints.Products.V1.GetImage,
            new
            {
                id = result.Value
            });
    }

    public static async Task<IResult> UpdateProduct(
        Guid id,
        UpdateProductEndpointRequest endpointRequest,
        IUpdateProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.CreatedAtRoute(
            ApiEndpoints.Products.V1.GetById,
            response,
            new
            {
                id = response.Id
            });
    }

    public static async Task<IResult> DeleteProduct(
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
            return Results.NotFound(result.ToApiError());
        }

        return Results.NoContent();
    }
}
