using Domain.UseCases.ProductCategories.Requests;

namespace Domain.UseCases.ProductCategories
{
    public interface IDeleteProductCategoryUseCase
    {
        Task<int?> ExecuteAsync(
            DeleteProductCategoryUseCaseRequest request,
            CancellationToken cancellationToken);
    }
}