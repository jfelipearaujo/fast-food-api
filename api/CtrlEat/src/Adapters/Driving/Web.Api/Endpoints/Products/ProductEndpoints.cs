using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;

using Mapster;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.Products.Requests;
using Web.Api.Endpoints.Products.Responses;

namespace Web.Api.Endpoints.Products
{
    public static class ProductEndpoints
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/product");

            group.MapGet("{id}", GetProductById)
                .WithName(nameof(GetProductById))
                .WithOpenApi();

            group.MapPost("/", CreateProduct)
                .WithName(nameof(CreateProduct))
                .WithOpenApi();
        }

        public static async Task<Results<Ok<ProductEndpointResponse>, NotFound<string>>> GetProductById(
            Guid id,
            IGetProductByIdUseCase getProductByIdUseCase,
            CancellationToken cancellationToken)
        {
            var request = new GetProductByIdRequest
            {
                Id = id
            };

            var result = await getProductByIdUseCase.ExecuteAsync(request, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Not Found");
            }

            var response = result.Adapt<ProductEndpointResponse>();

            return TypedResults.Ok(response);
        }

        public static async Task<Results<CreatedAtRoute<ProductEndpointResponse>, NotFound<string>>> CreateProduct(
            CreateProductEndpointRequest endpointRequest,
            ICreateProductUseCase createProductUseCase,
            CancellationToken cancellationToken)
        {
            var request = endpointRequest.Adapt<CreateProductRequest>();

            var result = await createProductUseCase.ExecuteAsync(request, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            var response = result.Adapt<ProductEndpointResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductById),
                new
                {
                    id = response.Id
                });
        }
    }
}
