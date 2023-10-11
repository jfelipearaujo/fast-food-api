using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.UpdateProduct.Requests;
using FluentResults;

namespace Domain.UseCases.Products.UpdateProduct;

public interface IUpdateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken);
}
