using Domain.Adapters;
using Domain.Errors.ProductCategories;
using Domain.Errors.Products;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

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

        public async Task<Result<ProductResponse>> ExecuteAsync(
            UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
            {
                return Result.Fail(new ProductNotFoundError(request.ProductId));
            }

            if (product.ProductCategoryId != request.ProductCategoryId)
            {
                var productCategory = await productCategoryRepository.GetByIdAsync(request.ProductCategoryId, cancellationToken);

                if (productCategory is null)
                {
                    return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
                }

                product.ProductCategory = productCategory;
            }

            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.Currency = request.Currency;
            product.ImageUrl = request.ImageUrl;

            await productRepository.UpdateAsync(product, cancellationToken);

            var response = product.Adapt<ProductResponse>();

            return Result.Ok(response);
        }
    }
}
