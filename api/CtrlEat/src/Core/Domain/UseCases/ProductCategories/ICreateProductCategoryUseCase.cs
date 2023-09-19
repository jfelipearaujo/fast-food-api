using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Domain.UseCases.ProductCategories
{
    public interface ICreateProductCategoryUseCase
    {
        Task<Result<ProductCategoryResponse>> ExecuteAsync(
            CreateProductCategoryRequest request,
            CancellationToken cancellationToken);
    }
}