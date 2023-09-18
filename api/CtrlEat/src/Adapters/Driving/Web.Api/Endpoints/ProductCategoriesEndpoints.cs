using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using Mapster;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.Requests;
using Web.Api.Endpoints.Responses;

namespace Web.Api.Endpoints
{
    public static class ProductCategoriesEndpoints
    {
        public static void MapProductCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/product/category");

            group.MapGet("{id}", GetProductCategory)
                .WithName(nameof(GetProductCategory))
                .WithOpenApi();

            group.MapGet("/", GetAllProductCategory)
                .WithName(nameof(GetAllProductCategory))
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

        public static async Task<Results<Ok<ProductCategoryResponse>, NotFound<string>>> GetProductCategory(
            Guid id,
            IGetProductCategoryUseCase getProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = new GetProductCategoryUseCaseRequest
            {
                Id = id
            };

            var result = await getProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            var response = result.Adapt<ProductCategoryResponse>();

            return TypedResults.Ok(response);
        }

        public static async Task<Ok<IEnumerable<ProductCategoryResponse>>> GetAllProductCategory(
            IGetAllProductCategoryUseCase getAllProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var result = await getAllProductCategoryUseCase.ExecuteAsync(cancellationToken);

            var response = result.Adapt<IEnumerable<ProductCategoryResponse>>();

            return TypedResults.Ok(response);
        }

        public static async Task<CreatedAtRoute<ProductCategoryResponse>> CreateProductCategory(
            CreateProductCategoryRequest request,
            ICreateProductCategoryUseCase createProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = request.Adapt<CreateProductCategoryUseCaseRequest>();

            var result = await createProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

            var response = result.Adapt<ProductCategoryResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductCategory),
                new
                {
                    id = response.Id,
                });
        }

        public static async Task<Results<CreatedAtRoute<ProductCategoryResponse>, NotFound<string>>> UpdateProductCategory(
            Guid id,
            UpdateProductCategoryRequest request,
            IUpdateProductCategoryUseCase updateProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = request.Adapt<UpdateProductCategoryUseCaseRequest>();

            useCaseRequest.Id = id;

            var result = await updateProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

            if (result is null)
            {
                return TypedResults.NotFound("Product Category Not Found");
            }

            var response = result.Adapt<ProductCategoryResponse>();

            return TypedResults.CreatedAtRoute(
                response,
                nameof(GetProductCategory), new
                {
                    id = response.Id,
                });
        }

        public static async Task<Results<NoContent, NotFound<string>>> DeleteProductCategory(
            Guid id,
            IDeleteProductCategoryUseCase deleteProductCategoryUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseRequest = new DeleteProductCategoryUseCaseRequest
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