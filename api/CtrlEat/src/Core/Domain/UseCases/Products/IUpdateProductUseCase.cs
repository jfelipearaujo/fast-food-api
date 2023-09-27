using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products;

public interface IUpdateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken);
}
