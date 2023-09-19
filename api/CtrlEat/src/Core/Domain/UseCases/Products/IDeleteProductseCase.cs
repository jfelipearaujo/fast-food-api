using Domain.UseCases.Products.Requests;

namespace Domain.UseCases.Products
{
    public interface IDeleteProductUseCase
    {
        Task<int?> ExecuteAsync(
            DeleteProductRequest request,
            CancellationToken cancellationToken);
    }
}
