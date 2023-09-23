using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.StrongIds;
using Domain.Errors.ProductCategories;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;
using Domain.ValueObjects;

using FluentResults;

namespace Application.UseCases.Products
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public CreateProductUseCase(
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<Result<ProductResponse>> ExecuteAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var productCategory = await productCategoryRepository.GetByIdAsync(ProductCategoryId.Create(request.ProductCategoryId), cancellationToken);

            if (productCategory is null)
            {
                return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
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

            var product = new Product
            {
                Id = ProductId.Create(Guid.NewGuid()),
                Description = request.Description,
                Currency = currency,
                Amount = currencyAmount,
                ImageUrl = request.ImageUrl,

                ProductCategory = productCategory,
            };

            await productRepository.CreateAsync(product, cancellationToken);

            var response = ProductResponse.MapFromDomain(product);

            return Result.Ok(response);
        }
    }
}
