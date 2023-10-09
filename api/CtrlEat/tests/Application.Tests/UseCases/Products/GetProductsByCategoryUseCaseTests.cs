using Application.UseCases.Products.GetProductsByCategory;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetProductsByCategory.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class GetProductsByCategoryUseCaseTests
{
    private readonly GetProductsByCategoryUseCase sut;

    private readonly IProductRepository repository;

    public GetProductsByCategoryUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();

        sut = new GetProductsByCategoryUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetProductsByCategorySuccessfully()
    {
        // Arrange
        var request = new GetProductsByCategoryRequest
        {
            Category = "category"
        };

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
            .GetAllByCategoryAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(new List<Product>
            {
                productOne,
                productTwo
            });

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

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
}
