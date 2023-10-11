using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.UpdateProductCategory.Request;
using FluentResults;

namespace Domain.UseCases.ProductCategories.UpdateProductCategory;

public interface IUpdateProductCategoryUseCase
{
    Task<Result<ProductCategoryResponse>> ExecuteAsync(
        UpdateProductCategoryRequest request,
        CancellationToken cancellationToken);
}