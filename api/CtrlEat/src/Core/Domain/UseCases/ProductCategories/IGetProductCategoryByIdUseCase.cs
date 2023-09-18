using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetProductCategoryByIdUseCase
    {
        Task<ProductCategoryResponse?> ExecuteAsync(
            GetProductCategoryByIdRequest request,
            CancellationToken cancellationToken);
    }
}