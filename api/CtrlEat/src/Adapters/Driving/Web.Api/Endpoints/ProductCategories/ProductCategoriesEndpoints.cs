using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using Mapster;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.ProductCategories.Requests;
using Web.Api.Endpoints.ProductCategories.Responses;

namespace Web.Api.Endpoints.ProductCategories
{
    public static class ProductCategoriesEndpoints
    {
        private const string EndpointTag = "Product Category";

        public static void MapProductCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/product/category")
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

        public static async Task<Results<Ok<ProductCategoryEndpointResponse>, NotFound<string>>> GetProductCategoryById(
            Guid id,
            IGetProductCategoryByIdUseCase getProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = new GetProductCategoryByIdRequest
            {
                Id = id
            };

            var result = await getProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            var response = result.Adapt<ProductCategoryEndpointResponse>();

            return TypedResults.Ok(response);
        }

        public static async Task<Ok<IEnumerable<ProductCategoryEndpointResponse>>> GetAllProductCategories(
            IGetAllProductCategoriesUseCase getAllProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var result = await getAllProductCategoryUseCase.ExecuteAsync(cancellationToken);

            var response = result.Adapt<IEnumerable<ProductCategoryEndpointResponse>>();

            return TypedResults.Ok(response);
        }

        public static async Task<CreatedAtRoute<ProductCategoryEndpointResponse>> CreateProductCategory(
            CreateProductCategoryEndpointRequest endpointRequest,
            ICreateProductCategoryUseCase createProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var request = new CreateProductCategoryRequest
            {
                Description = endpointRequest.Description,
            };

            var result = await createProductCategoryUseCase.ExecuteAsync(request, cancellationToken);

            var response = result.Adapt<ProductCategoryEndpointResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductCategoryById),
                new
                {
                    id = response.Id,
                });
        }

        public static async Task<Results<CreatedAtRoute<ProductCategoryEndpointResponse>, NotFound<string>>> UpdateProductCategory(
            Guid id,
            UpdateProductCategoryEndpointRequest endpointRequest,
            IUpdateProductCategoryUseCase updateProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var request = new UpdateProductCategoryRequest
            {
                Id = id,
                Description = endpointRequest.Description,
            };

            var result = await updateProductCategoryUseCase.ExecuteAsync(request, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            var response = result.Adapt<ProductCategoryEndpointResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductCategoryById),
                new
                {
                    id = response.Id,
                });
        }

        public static async Task<Results<NoContent, NotFound<string>>> DeleteProductCategory(
            Guid id,
            IDeleteProductCategoryUseCase deleteProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = new DeleteProductCategoryRequest
            {
                Id = id
            };

            var result = await deleteProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            return TypedResults.NoContent();
        }
    }
}