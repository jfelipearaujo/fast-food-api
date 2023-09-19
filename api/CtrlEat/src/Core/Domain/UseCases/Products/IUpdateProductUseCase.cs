using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Domain.UseCases.Products
{
    public interface IUpdateProductUseCase
    {
        Task<ProductResponse?> ExecuteAsync(
            UpdateProductRequest request,
            CancellationToken cancellationToken);
    }
}
