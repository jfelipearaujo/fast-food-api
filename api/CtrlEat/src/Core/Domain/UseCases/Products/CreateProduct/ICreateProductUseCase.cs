using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.CreateProduct.Requests;

using FluentResults;

namespace Domain.UseCases.Products.CreateProduct;

public interface ICreateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken);
}
