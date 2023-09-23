using Domain.Adapters;
using Domain.Entities.TypedIds;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using FluentResults;

namespace Application.UseCases.ProductCategories
{
    public class DeleteProductCategoryUseCase : IDeleteProductCategoryUseCase
    {
        private readonly IProductCategoryRepository repository;

        public DeleteProductCategoryUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<int>> ExecuteAsync(
            DeleteProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var productCategory = await repository.GetByIdAsync(new ProductCategoryId(request.Id), cancellationToken);

            if (productCategory is null)
                return Result.Fail(new ProductCategoryNotFoundError(request.Id));

            var deletedEntities = await repository.DeleteAsync(productCategory, cancellationToken);

            return Result.Ok(deletedEntities);
        }
    }
}