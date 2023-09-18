using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class GetAllProductCategoryUseCase : IGetAllProductCategoryUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public GetAllProductCategoryUseCase(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<GetProductCategoryUseCaseResponse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var productCategories = await _productCategoryRepository.GetAllAsync(cancellationToken);

            return productCategories.Adapt<IEnumerable<GetProductCategoryUseCaseResponse>>();
        }
    }
}