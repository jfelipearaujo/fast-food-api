using Application.UseCases.Common.Errors;
using Application.UseCases.Products.CreateProduct;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.CreateProduct.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class CreateProductUseCaseTests
{
    private readonly CreateProductUseCase sut;

    private readonly IProductRepository productRepository;
    private readonly IProductCategoryRepository productCategoryRepository;

    public CreateProductUseCaseTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        productCategoryRepository = Substitute.For<IProductCategoryRepository>();


        sut = new CreateProductUseCase(productRepository, productCategoryRepository);
    }

    [Fact]
    public async Task ShouldCreateProductSuccessfully()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            ProductCategoryId = Guid.NewGuid(),
            Description = "Product",
            Amount = 1.0m,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .WithId(request.ProductCategoryId)
            .Build();

        var product = new ProductBuilder()
            .WithDescription(request.Description)
            .WithProductCategory(productCategory)
            .WithPrice(request.Currency, request.Amount)
            .WithImageUrl(request.ImageUrl)
            .Build();

        productCategoryRepository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(productCategory);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        var expectedResponse = ProductResponse.MapFromDomain(product);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
            result.Value.Id.Should().NotBeEmpty();
        });

        await productCategoryRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Is<ProductCategoryId>(x => x.Value == request.ProductCategoryId),
                Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .CreateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleIfProductCategoryWasNotFound()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            ProductCategoryId = Guid.NewGuid(),
            Description = "Product",
            Amount = 1.0m,
            Currency = "BRL",
            ImageUrl = "http://image.com/123.png"
        };

        productCategoryRepository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(default(ProductCategory));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.ProductCategoryId));

        await productCategoryRepository
            .Received(1)
            .GetByIdAsync(
                Arg.Is<ProductCategoryId>(x => x.Value == request.ProductCategoryId),
                Arg.Any<CancellationToken>());

        await productRepository
            .DidNotReceive()
            .CreateAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }
}
