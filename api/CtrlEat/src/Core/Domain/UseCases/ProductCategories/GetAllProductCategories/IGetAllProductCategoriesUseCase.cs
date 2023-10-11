using Domain.UseCases.ProductCategories.Common.Responses;
using FluentResults;

namespace Domain.UseCases.ProductCategories.GetAllProductCategories;

public interface IGetAllProductCategoriesUseCase
{
    Task<Result<List<ProductCategoryResponse>>> ExecuteAsync(
        CancellationToken cancellationToken);
}