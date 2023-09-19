using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetProductCategoryByIdUseCase
    {
        Task<Result<ProductCategoryResponse>> ExecuteAsync(
            GetProductCategoryByIdRequest request,
            CancellationToken cancellationToken);
    }
}