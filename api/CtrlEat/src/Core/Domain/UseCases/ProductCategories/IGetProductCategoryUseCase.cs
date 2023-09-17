using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetProductCategoryUseCase
    {
        Task<GetProductCategoryUseCaseResponse?> ExecuteAsync(
            GetProductCategoryUseCaseRequest request,
            CancellationToken cancellationToken);
    }
}