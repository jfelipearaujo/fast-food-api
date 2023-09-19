using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetAllProductCategoriesUseCase
    {
        Task<IEnumerable<ProductCategoryResponse>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}