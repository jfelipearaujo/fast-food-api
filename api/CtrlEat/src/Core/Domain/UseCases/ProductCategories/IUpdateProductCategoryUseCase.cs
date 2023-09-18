using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IUpdateProductCategoryUseCase
    {
        Task<ProductCategoryResponse?> ExecuteAsync(
            UpdateProductCategoryRequest request,
            CancellationToken cancellationToken);
    }
}