using Domain.UseCases.Products.DeleteProduct.Requests;
using FluentResults;

namespace Domain.UseCases.Products.DeleteProduct;

public interface IDeleteProductUseCase
{
    Task<Result<int>> ExecuteAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken);
}
