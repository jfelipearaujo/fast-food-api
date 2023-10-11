using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetProductById.Requests;

using FluentResults;

namespace Domain.UseCases.Products.GetProductById;

public interface IGetProductByIdUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken);
}
