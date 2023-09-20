using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products
{
    public interface IGetProductsByCategoryUseCase
    {
        Task<Result<IEnumerable<ProductResponse>>> ExecuteAsync(
            GetProductsByCategoryRequest request,
            CancellationToken cancellationToken);
    }
}
