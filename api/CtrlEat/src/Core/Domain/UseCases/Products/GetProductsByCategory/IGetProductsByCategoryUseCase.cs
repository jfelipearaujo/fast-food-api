using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetProductsByCategory.Requests;

using FluentResults;

namespace Domain.UseCases.Products.GetProductsByCategory;

public interface IGetProductsByCategoryUseCase
{
    Task<Result<List<ProductResponse>>> ExecuteAsync(
        GetProductsByCategoryRequest request,
        CancellationToken cancellationToken);
}
