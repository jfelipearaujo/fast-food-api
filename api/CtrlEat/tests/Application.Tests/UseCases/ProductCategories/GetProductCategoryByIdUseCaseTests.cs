using Application.UseCases.Common.Errors;
using Application.UseCases.ProductCategories.GetProductCategoryById;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.GetProductCategoryById.Request;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.ProductCategories;

public class GetProductCategoryByIdUseCaseTests
{
    private readonly GetProductCategoryByIdUseCase sut;

    private readonly IProductCategoryRepository repository;

    public GetProductCategoryByIdUseCaseTests()
    {
        repository = Substitute.For<IProductCategoryRepository>();

        sut = new GetProductCategoryByIdUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetProductCategorySuccessfully()
    {
        // Arrange
        var request = new GetProductCategoryByIdRequest
        {
            Id = Guid.NewGuid()
        };

        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .WithId(request.Id)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(productCategory);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(new ProductCategoryResponse
            {
                Id = request.Id,
                Description = "Product Category"
            });
        });
    }

    [Fact]
    public async Task ShouldHandleWhenNothingWasFound()
    {
        // Arrange
        var request = new GetProductCategoryByIdRequest
        {
            Id = Guid.NewGuid()
        };

        repository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(default(ProductCategory));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.Id));
    }
}
