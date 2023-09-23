using Domain.Adapters;
using Domain.Entities.TypedIds;
using Domain.Errors.ProductCategories;
using Domain.Errors.Products;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;
using Domain.ValueObjects;

using FluentResults;

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
            var product = await productRepository.GetByIdAsync(
                new ProductId(request.ProductId),
                cancellationToken);

            if (product is null)
            {
                return Result.Fail(new ProductNotFoundError(request.ProductId));
            }

            if (product.ProductCategoryId != new ProductCategoryId(request.ProductCategoryId))
            {
                var productCategory = await productCategoryRepository.GetByIdAsync(
                    new ProductCategoryId(request.ProductCategoryId),
                    cancellationToken);

                if (productCategory is null)
                {
                    return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
                }

                product.ProductCategory = productCategory;
            }

            var price = Money.Create(request.Currency, request.Amount);

            if (price.IsFailed)
            {
                return Result.Fail(price.Errors);
            }

            product.Description = request.Description;
            product.Price = price.Value;
            product.ImageUrl = request.ImageUrl;

            await productRepository.UpdateAsync(product, cancellationToken);

            var response = ProductResponse.MapFromDomain(product);

            return Result.Ok(response);
        }
    }
}