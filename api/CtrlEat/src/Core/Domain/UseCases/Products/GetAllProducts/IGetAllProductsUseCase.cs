using Domain.UseCases.Products.Common.Responses;
using FluentResults;

namespace Domain.UseCases.Products.GetAllProducts;

public interface IGetAllProductsUseCase
{
    Task<Result<List<ProductResponse>>> ExecuteAsync(
        CancellationToken cancellationToken);
}
