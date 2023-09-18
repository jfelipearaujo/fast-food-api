using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class UpdateProductCategoryUseCase : IUpdateProductCategoryUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public UpdateProductCategoryUseCase(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryResponse?> ExecuteAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (productCategory is null)
                return null;

            productCategory.Description = request.Description;

            await _productCategoryRepository.UpdateAsync(productCategory, cancellationToken);

            return productCategory.Adapt<ProductCategoryResponse>();
        }
    }
}