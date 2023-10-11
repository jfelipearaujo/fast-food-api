using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.GetProductCategoryById.Request;

using FluentResults;

namespace Domain.UseCases.ProductCategories.GetProductCategoryById;

public interface IGetProductCategoryByIdUseCase
{
    Task<Result<ProductCategoryResponse>> ExecuteAsync(
        GetProductCategoryByIdRequest request,
        CancellationToken cancellationToken);
}