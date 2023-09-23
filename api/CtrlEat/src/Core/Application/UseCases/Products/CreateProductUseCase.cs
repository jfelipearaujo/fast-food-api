using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;
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
            var productCategory = await productCategoryRepository.GetByIdAsync(new ProductCategoryId(request.ProductCategoryId), cancellationToken);

            if (productCategory is null)
            {
                return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
            }

            var price = Money.Create(request.Currency, request.Amount);

            if (price.IsFailed)
            {
                return Result.Fail(price.Errors);
            }

            var product = new Product
            {
                Id = new ProductId(Guid.NewGuid()),
                ProductCategoryId = productCategory.Id,
                ProductCategory = productCategory,
                Description = request.Description,
                Price = price.Value,
                ImageUrl = request.ImageUrl,
            };

            await productRepository.CreateAsync(product, cancellationToken);

            var response = ProductResponse.MapFromDomain(product);

            return Result.Ok(response);
        }
    }
}
