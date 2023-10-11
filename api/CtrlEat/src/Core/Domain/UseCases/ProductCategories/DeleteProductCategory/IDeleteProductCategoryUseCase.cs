using Domain.UseCases.ProductCategories.DeleteProductCategory.Request;
using FluentResults;

namespace Domain.UseCases.ProductCategories.DeleteProductCategory;

public interface IDeleteProductCategoryUseCase
{
    Task<Result<int>> ExecuteAsync(
        DeleteProductCategoryRequest request,
        CancellationToken cancellationToken);
}