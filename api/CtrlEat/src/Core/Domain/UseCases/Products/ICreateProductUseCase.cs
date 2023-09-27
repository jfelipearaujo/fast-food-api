using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Domain.UseCases.Products;

public interface ICreateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken);
}
