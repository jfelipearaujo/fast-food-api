using Domain.Adapters;
using Domain.Entities;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class CreateProductCategoryUseCase : ICreateProductCategoryUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public CreateProductCategoryUseCase(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryResponse> ExecuteAsync(CreateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var productsCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
            };

            await _productCategoryRepository.CreateAsync(productsCategory, cancellationToken);

            return productsCategory.Adapt<ProductCategoryResponse>();
        }
    }
}