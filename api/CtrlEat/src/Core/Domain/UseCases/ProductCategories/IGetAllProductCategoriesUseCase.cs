using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetAllProductCategoriesUseCase
    {
        Task<Result<IEnumerable<ProductCategoryResponse>>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}