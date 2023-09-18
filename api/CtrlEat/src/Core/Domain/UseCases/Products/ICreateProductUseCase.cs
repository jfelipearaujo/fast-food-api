using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Domain.UseCases.Products
{
    public interface ICreateProductUseCase
    {
        Task<ProductResponse?> ExecuteAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken);
    }
}
