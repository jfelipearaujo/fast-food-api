using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products
{
    public interface IGetProductByIdUseCase
    {
        Task<Result<ProductResponse>> ExecuteAsync(
            GetProductByIdRequest request,
            CancellationToken cancellationToken);
    }
}
