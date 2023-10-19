using Application.UseCases.ProductCategories.GetAllProductCategories;
using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate;
using Domain.UseCases.ProductCategories.Common.Responses;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.ProductCategories;

public class GetAllProductCategoryUseCaseTests
{
    private readonly GetAllProductCategoriesUseCase sut;

    private readonly IProductCategoryRepository repository;

    public GetAllProductCategoryUseCaseTests()
    {
        repository = Substitute.For<IProductCategoryRepository>();

        sut = new GetAllProductCategoriesUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetAllProductCategorySuccessfully()
    {
        // Arrange
        var productCategoryOne = new ProductCategoryBuilder().WithSample().Build();
        var productCategoryTwo = new ProductCategoryBuilder().WithSample().Build();

        repository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(new List<ProductCategory>
            {
                productCategoryOne,
                productCategoryTwo
            });

        // Act
        var response = await sut.ExecuteAsync(cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(new List<ProductCategoryResponse>
            {
                ProductCategoryResponse.MapFromDomain(productCategoryOne),
                ProductCategoryResponse.MapFromDomain(productCategoryTwo),
            });
        });
    }

    [Fact]
    public async Task ShouldReturnNothingIfNothingWasFound()
    {
        // Arrange
        repository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(new List<ProductCategory>());

        // Act
        var response = await sut.ExecuteAsync(cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(Enumerable.Empty<ProductCategoryResponse>());
        });
    }
}
