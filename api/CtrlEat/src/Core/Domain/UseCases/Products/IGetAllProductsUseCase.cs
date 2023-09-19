using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products
{
    public interface IGetAllProductsUseCase
    {
        Task<Result<IEnumerable<ProductResponse>>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}
