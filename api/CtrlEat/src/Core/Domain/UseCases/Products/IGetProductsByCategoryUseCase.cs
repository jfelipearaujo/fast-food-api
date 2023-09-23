using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products
{
    public interface IGetProductsByCategoryUseCase
    {
        Task<Result<List<ProductResponse>>> ExecuteAsync(
            GetProductsByCategoryRequest request,
            CancellationToken cancellationToken);
    }
}
