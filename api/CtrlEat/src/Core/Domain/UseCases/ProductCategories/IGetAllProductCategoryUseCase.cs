using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface IGetAllProductCategoryUseCase
    {
        Task<IEnumerable<GetProductCategoryUseCaseResponse>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}