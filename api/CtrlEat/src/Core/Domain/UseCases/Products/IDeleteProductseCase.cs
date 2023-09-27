using Domain.UseCases.Products.Requests;

using FluentResults;

namespace Domain.UseCases.Products;

public interface IDeleteProductUseCase
{
    Task<Result<int>> ExecuteAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken);
}
