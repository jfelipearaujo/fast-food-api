using Domain.UseCases.ProductCategories.Requests;

using FluentResults;

namespace Domain.UseCases.ProductCategories
{
    public interface IDeleteProductCategoryUseCase
    {
        Task<Result<int>> ExecuteAsync(
            DeleteProductCategoryRequest request,
            CancellationToken cancellationToken);
    }
}