using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetAllProductCategoryUseCase
    {
        Task<IEnumerable<ProductCategoryResponse>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}