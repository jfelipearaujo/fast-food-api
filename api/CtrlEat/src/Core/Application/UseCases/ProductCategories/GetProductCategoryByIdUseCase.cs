using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class GetProductCategoryByIdUseCase : IGetProductCategoryByIdUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public GetProductCategoryByIdUseCase(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryResponse?> ExecuteAsync(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (productCategory is null)
                return null;

            return productCategory.Adapt<ProductCategoryResponse>();
        }
    }
}