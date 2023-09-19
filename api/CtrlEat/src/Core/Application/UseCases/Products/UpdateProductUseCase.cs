using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using Mapster;

namespace Application.UseCases.Products
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public UpdateProductUseCase(
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductResponse?> ExecuteAsync(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
                return null;

            if (product.ProductCategoryId != request.ProductCategoryId)
            {
                var productCategory = await productCategoryRepository.GetByIdAsync(request.ProductCategoryId, cancellationToken);

                if (productCategory is null)
                    return null;

                product.ProductCategory = productCategory;
            }

            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.Currency = request.Currency;
            product.ImageUrl = request.ImageUrl;

            await productRepository.UpdateAsync(product, cancellationToken);

            return product.Adapt<ProductResponse>();
        }
    }
}
