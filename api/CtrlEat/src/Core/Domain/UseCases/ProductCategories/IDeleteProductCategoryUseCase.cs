using Domain.UseCases.ProductCategories.Requests;

namespace Domain.UseCases.ProductCategories
{
    public interface IDeleteProductCategoryUseCase
    {
        Task<int?> ExecuteAsync(
            DeleteProductCategoryRequest request,
            CancellationToken cancellationToken);
    }
}