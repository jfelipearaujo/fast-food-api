using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Application.UseCases.Products.CreateProduct;

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
        var productCategory = await productCategoryRepository.GetByIdAsync(
            ProductCategoryId.Create(request.ProductCategoryId),
            cancellationToken);

        if (productCategory is null)
        {
            return Result.Fail(new ProductCategoryNotFoundError(request.ProductCategoryId));
        }

        var price = Money.Create(request.Amount, request.Currency);

        if (price.IsFailed)
        {
            return Result.Fail(price.Errors);
        }

        var product = Product.Create(
            request.Description,
            price.Value,
            request.ImageUrl,
            productCategory
        );

        await productRepository.CreateAsync(product.Value, cancellationToken);

        return ProductResponse.MapFromDomain(product.Value);
    }
}
