using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.UpdateProduct;
using Domain.UseCases.Products.UpdateProduct.Requests;
using FluentResults;

namespace Application.UseCases.Products.UpdateProduct;

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
            ProductId.Create(request.ProductCategoryId),
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

        var price = Money.Create(request.Amount, request.Currency);

        if (price.IsFailed)
        {
            return Result.Fail(price.Errors);
        }

        product.Update(request.Description, price.Value, request.ImageUrl);

        await productRepository.UpdateAsync(product, cancellationToken);

        var response = ProductResponse.MapFromDomain(product);

        return Result.Ok(response);
    }
}