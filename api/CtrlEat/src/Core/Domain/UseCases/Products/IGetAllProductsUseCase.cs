using Domain.UseCases.Products.Responses;

namespace Domain.UseCases.Products
{
    public interface IGetAllProductsUseCase
    {
        Task<IEnumerable<ProductResponse>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}
