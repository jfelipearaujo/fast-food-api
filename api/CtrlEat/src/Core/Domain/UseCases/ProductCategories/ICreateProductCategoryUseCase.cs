using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface ICreateProductCategoryUseCase
    {
        Task<ProductCategoryResponse> ExecuteAsync(
            CreateProductCategoryRequest request,
            CancellationToken cancellationToken);
    }
}