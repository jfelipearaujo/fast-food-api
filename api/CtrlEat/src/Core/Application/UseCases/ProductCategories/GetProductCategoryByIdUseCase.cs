using Domain.Adapters;
using Domain.Entities.TypedIds;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Application.UseCases.ProductCategories
{
    public class GetProductCategoryByIdUseCase : IGetProductCategoryByIdUseCase
    {
        private readonly IProductCategoryRepository repository;

        public GetProductCategoryByIdUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<ProductCategoryResponse>> ExecuteAsync(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await repository.GetByIdAsync(new ProductCategoryId(request.Id), cancellationToken);

            if (productCategory is null)
                return Result.Fail(new ProductCategoryNotFoundError(request.Id));

            var response = ProductCategoryResponse.MapFromDomain(productCategory);

            return Result.Ok(response);
        }
    }
}