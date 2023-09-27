using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Domain.UseCases.ProductCategories;

public interface IGetAllProductCategoriesUseCase
{
    Task<Result<List<ProductCategoryResponse>>> ExecuteAsync(
        CancellationToken cancellationToken);
}