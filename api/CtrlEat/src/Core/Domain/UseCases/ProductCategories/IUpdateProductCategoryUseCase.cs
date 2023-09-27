using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Domain.UseCases.ProductCategories;

public interface IUpdateProductCategoryUseCase
{
    Task<Result<ProductCategoryResponse>> ExecuteAsync(
        UpdateProductCategoryRequest request,
        CancellationToken cancellationToken);
}