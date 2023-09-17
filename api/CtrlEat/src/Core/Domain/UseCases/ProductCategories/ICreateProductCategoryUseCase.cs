using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.ProductCategories
{
    public interface ICreateProductCategoryUseCase
    {
        Task<CreateProductCategoryUseCaseResponse> ExecuteAsync(
            CreateProductCategoryUseCaseRequest request,
            CancellationToken cancellationToken);
    }
}