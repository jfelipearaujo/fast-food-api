using Application.UseCases.Products.GetAllProducts;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.UseCases.Products.Responses;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class GetAllProductsUseCaseTests
{
    private readonly GetAllProductsUseCase sut;

    private readonly IProductRepository repository;

    public GetAllProductsUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();

        sut = new GetAllProductsUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetAllProductsSuccessfully()
    {
        // Arrange
        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        var productOne = new ProductBuilder()
            .WithSample()
            .WithProductCategory(productCategory)
            .Build();

        var productTwo = new ProductBuilder()
            .WithSample()
            .WithProductCategory(productCategory)
            .Build();

        repository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(new List<Product>
            {
                productOne,
                productTwo
            });

        // Act
        var response = await sut.ExecuteAsync(cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(new List<ProductResponse>
            {
                ProductResponse.MapFromDomain(productOne),
                ProductResponse.MapFromDomain(productTwo),
            });
        });
    }

    [Fact]
    public async Task ShouldReturnNothingIfNothingWasFound()
    {
        // Arrange
        repository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(new List<Product>());

        // Act
        var response = await sut.ExecuteAsync(cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(Enumerable.Empty<ProductResponse>());
        });
    }
}
