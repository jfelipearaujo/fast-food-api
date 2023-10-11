using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.CreateProductCategory.Request;

using FluentResults;

namespace Domain.UseCases.ProductCategories.CreateProductCategory;

public interface ICreateProductCategoryUseCase
{
    Task<Result<ProductCategoryResponse>> ExecuteAsync(
        CreateProductCategoryRequest request,
        CancellationToken cancellationToken);
}