using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IUpdateProductCategoryUseCase
    {
        Task<UpdateProductCategoryUseCaseResponse?> ExecuteAsync(
            UpdateProductCategoryUseCaseRequest request,
            CancellationToken cancellationToken);
    }
}