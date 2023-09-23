using Domain.Adapters;
using Domain.Entities.StrongIds;
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
                ProductId.Create(request.ProductId),
                cancellationToken);

            if (product is null)
            {
                return Result.Fail(new ProductNotFoundError(request.ProductId));
            }

            if (product.ProductCategoryId != ProductCategoryId.Create(request.ProductCategoryId))
            {
                var productCategory = await productCategoryRepository.GetByIdAsync(
                    ProductCategoryId.Create(request.ProductCategoryId),
                    cancellationToken);

                if (productCategory is null)
                {
                    return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
                }

                product.ProductCategory = productCategory;
            }

            var currency = new Currency(request.Currency);
            var currencyValiation = currency.Validate();

            if (currencyValiation.IsFailed)
            {
                return Result.Fail(currencyValiation.Errors);
            }

            var currencyAmount = new CurrencyAmount(request.Amount);
            var currencyAmountValiation = currencyAmount.Validate();

            if (currencyAmountValiation.IsFailed)
            {
                return Result.Fail(currencyAmountValiation.Errors);
            }

            product.Description = request.Description;
            product.Currency = currency;
            product.Amount = currencyAmount;
            product.ImageUrl = request.ImageUrl;

            await productRepository.UpdateAsync(product, cancellationToken);

            var response = ProductResponse.MapFromDomain(product);

            return Result.Ok(response);
        }
    }
}