using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Domain.UseCases.Products
{
    public interface IGetProductByIdUseCase
    {
        Task<ProductResponse?> ExecuteAsync(
            GetProductByIdRequest request,
            CancellationToken cancellationToken);
    }
}
