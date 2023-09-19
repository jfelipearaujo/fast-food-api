using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.ProductCategories;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

using Mapster;

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
            var productCategory = await productCategoryRepository.GetByIdAsync(request.ProductCategoryId, cancellationToken);

            if (productCategory is null)
            {
                return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                ProductCategory = productCategory,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Currency = request.Currency,
                ImageUrl = request.ImageUrl,
            };

            await productRepository.CreateAsync(product, cancellationToken);

            var response = product.Adapt<ProductResponse>();

            return Result.Ok(response);
        }
    }
}
