using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;

using Mapster;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.Products.Requests;
using Web.Api.Endpoints.Products.Responses;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products
{
    public static class ProductEndpoints
    {
        private const string EndpointTag = "Product";

        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/product")
                .WithTags(EndpointTag);

            group.MapGet("{id}", GetProductById)
                .WithName(nameof(GetProductById))
                .WithOpenApi();

            group.MapGet("/", GetAllProducts)
                .WithName(nameof(GetAllProducts))
                .WithOpenApi();

            group.MapPost("/", CreateProduct)
                .WithName(nameof(CreateProduct))
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

            var response = result.Value.Adapt<ProductEndpointResponse>();

            return TypedResults.Ok(response);
        }

        public static async Task<Ok<IEnumerable<ProductEndpointResponse>>> GetAllProducts(
            IGetAllProductsUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(cancellationToken);

            var response = result.Value.Adapt<IEnumerable<ProductEndpointResponse>>();

            return TypedResults.Ok(response);
        }

        public static async Task<Results<CreatedAtRoute<ProductEndpointResponse>, NotFound<ApiError>>> CreateProduct(
            CreateProductEndpointRequest endpointRequest,
            ICreateProductUseCase useCase,
            CancellationToken cancellationToken)
        {
            var request = endpointRequest.Adapt<CreateProductRequest>();

            var result = await useCase.ExecuteAsync(request, cancellationToken);

            if (result.IsFailed)
            {
                return TypedResults.NotFound(result.ToApiError());
            }

            var response = result.Value.Adapt<ProductEndpointResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductById),
                new
                {
                    id = response.Id
                });
        }

        public static async Task<Results<CreatedAtRoute<ProductEndpointResponse>, NotFound<ApiError>>> UpdateProduct(
            Guid id,
            UpdateProductEndpointRequest endpointRequest,
            IUpdateProductUseCase useCase,
            CancellationToken cancellationToken)
        {
            var request = endpointRequest.Adapt<UpdateProductRequest>();

            var result = await useCase.ExecuteAsync(request, cancellationToken);

            if (result.IsFailed)
            {
                return TypedResults.NotFound(result.ToApiError());
            }

            var response = result.Value.Adapt<ProductEndpointResponse>();

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
}
