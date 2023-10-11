using Application.UseCases.Common.Errors;
using Application.UseCases.Products.UpdateProduct;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.UpdateProduct.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class UpdateProductUseCaseTests
{
    private readonly UpdateProductUseCase sut;

    private readonly IProductRepository productRepository;
    private readonly IProductCategoryRepository productCategoryRepository;

    public UpdateProductUseCaseTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        productCategoryRepository = Substitute.For<IProductCategoryRepository>();


        sut = new UpdateProductUseCase(productRepository, productCategoryRepository);
    }

    [Fact]
    public async Task ShouldUpdateProductDescriptionSuccessfully()
    {
        // Arrange
        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithDescription("Old Product Description")
            .WithProductCategory(productCategory)
            .Build();

        var request = new UpdateProductRequest
        {
            ProductId = product.Id.Value,
            ProductCategoryId = productCategory.Id.Value,
            Description = "New Product Description",
            Amount = 10,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        productCategoryRepository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(productCategory);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        var expectedResponse = ProductResponse.MapFromDomain(product);
        expectedResponse.Description = "New Product Description";

        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expectedResponse);
        });

        await productRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductId>(),
                Arg.Any<CancellationToken>());

        await productCategoryRepository
            .DidNotReceive()
            .GetByIdAsync(
                Arg.Any<ProductCategoryId>(),
                Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .UpdateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldUpdateProductCategorySuccessfully()
    {
        // Arrange
        var newProductCategoryId = Guid.NewGuid();

        var request = new UpdateProductRequest
        {
            ProductId = Guid.NewGuid(),
            ProductCategoryId = newProductCategoryId,
            Description = "Product Description",
            Amount = 10,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        var oldProductCategory = new ProductCategoryBuilder()
            .WithDescription("Old Product Category")
            .Build();

        var newProductCategory = new ProductCategoryBuilder()
            .WithId(newProductCategoryId)
            .WithDescription("New Product Category")
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithId(request.ProductId)
            .WithProductCategory(oldProductCategory)
            .Build();

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        productCategoryRepository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(newProductCategory);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        var expectedResponse = ProductResponse.MapFromDomain(product);
        expectedResponse.ProductCategory.Description = "New Product Category";

        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expectedResponse);
        });

        await productRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductId>(),
                Arg.Any<CancellationToken>());

        await productCategoryRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductCategoryId>(),
                Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .UpdateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenProductWasNotFound()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            ProductId = Guid.NewGuid(),
            ProductCategoryId = Guid.NewGuid(),
            Description = "New Product Description",
            Amount = 10,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(default(Product));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.ProductId));

        await productRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductId>(),
                Arg.Any<CancellationToken>());

        await productCategoryRepository
            .DidNotReceive()
            .GetByIdAsync(
                Arg.Any<ProductCategoryId>(),
                Arg.Any<CancellationToken>());

        await productRepository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenProductCategoryWasNotFound()
    {
        // Arrange
        var newProductCategoryId = ProductCategoryId.CreateUnique();

        var request = new UpdateProductRequest
        {
            ProductId = Guid.NewGuid(),
            ProductCategoryId = newProductCategoryId.Value,
            Description = "Product Description",
            Amount = 10,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithId(request.ProductId)
            .WithProductCategory(productCategory)
            .Build();

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        productCategoryRepository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(default(ProductCategory));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.ProductCategoryId));

        await productRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductId>(),
                Arg.Any<CancellationToken>());

        await productCategoryRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Any<ProductCategoryId>(),
                Arg.Any<CancellationToken>());

        await productRepository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }
}
