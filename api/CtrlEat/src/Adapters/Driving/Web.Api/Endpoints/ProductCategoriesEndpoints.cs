using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using Mapster;

using Web.Api.Endpoints.Names;
using Web.Api.Endpoints.Requests;
using Web.Api.Endpoints.Responses;

namespace Web.Api.Endpoints
{
    public static class ProductCategoriesEndpoints
    {
        public static void AddProductCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/product/category/{id}", async (Guid id,
                IGetProductCategoryUseCase _getProductCategoryUseCase,
                CancellationToken cancellationToken) =>
            {
                var useCaseRequest = new GetProductCategoryUseCaseRequest
                {
                    Id = id
                };

                var result = await _getProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

                if (result is null)
                {
                    return Results.NotFound();
                }

                var response = result.Adapt<ProductCategoryResponse>();

                return Results.Ok(response);
            })
                .WithName(ProductCategoriesEndpointNames.GetProductCategory)
                .WithOpenApi();

            app.MapGet("/product/category", async (
                IGetAllProductCategoryUseCase _getAllProductCategoryUseCase,
                CancellationToken cancellationToken) =>
            {
                var result = await _getAllProductCategoryUseCase.ExecuteAsync(cancellationToken);

                var response = result.Adapt<IEnumerable<ProductCategoryResponse>>();

                return Results.Ok(response);
            })
                .WithName(ProductCategoriesEndpointNames.GetAllProductCategory)
                .WithOpenApi();

            app.MapPost("/product/category", async (CreateProductCategoryRequest request,
                ICreateProductCategoryUseCase _createProductCategoryUseCase,
                CancellationToken cancellationToken) =>
            {
                var useCaseRequest = request.Adapt<CreateProductCategoryUseCaseRequest>();

                var result = await _createProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

                var response = result.Adapt<ProductCategoryResponse>();

                return Results.CreatedAtRoute(
                    ProductCategoriesEndpointNames.GetProductCategory, new
                    {
                        id = response.Id,
                    },
                    response);
            })
                .WithName(ProductCategoriesEndpointNames.CreateProductCategory)
                .WithOpenApi();

            app.MapPut("/product/category/{id}", async (
                Guid id,
                UpdateProductCategoryRequest request,
                IUpdateProductCategoryUseCase _updateProductCategoryUseCase,
                CancellationToken cancellationToken) =>
            {
                var useCaseRequest = request.Adapt<UpdateProductCategoryUseCaseRequest>();

                useCaseRequest.Id = id;

                var result = await _updateProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

                if (result is null)
                {
                    return Results.NotFound();
                }

                var response = result.Adapt<ProductCategoryResponse>();

                return Results.CreatedAtRoute(
                    ProductCategoriesEndpointNames.GetProductCategory, new
                    {
                        id = response.Id,
                    },
                    response);
            })
                .WithName(ProductCategoriesEndpointNames.UpdateProductCategory)
                .WithOpenApi();

            app.MapDelete("/product/category/{id}", async (Guid id,
               IDeleteProductCategoryUseCase _deleteProductCategoryUseCase,
               CancellationToken cancellationToken) =>
            {
                var useCaseRequest = new DeleteProductCategoryUseCaseRequest
                {
                    Id = id
                };

                var result = await _deleteProductCategoryUseCase.ExecuteAsync(useCaseRequest, cancellationToken);

                if (result is null)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
               .WithName(ProductCategoriesEndpointNames.DeleteProductCategory)
               .WithOpenApi();
        }
    }
}