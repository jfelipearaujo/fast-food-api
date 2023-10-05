using Application.UseCases.Common.Errors;
using Application.UseCases.ProductCategories.UpdateProductCategory;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.ProductCategories;

public class UpdateProductCategoryUseCaseTests
{
    private readonly UpdateProductCategoryUseCase sut;

    private readonly IProductCategoryRepository repository;

    public UpdateProductCategoryUseCaseTests()
    {
        repository = Substitute.For<IProductCategoryRepository>();

        sut = new UpdateProductCategoryUseCase(repository);
    }

    [Fact]
    public async Task ShouldUpdateProductCategorySuccessfully()
    {
        // Arrange
        var request = new UpdateProductCategoryRequest
        {
            Id = Guid.NewGuid(),
            Description = "New Product Description"
        };

        var productCategory = new ProductCategoryBuilder()
            .WithId(request.Id)
            .WithDescription(request.Description)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(productCategory);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(ProductCategoryResponse.MapFromDomain(productCategory));
        });

        await repository
            .Received(1)
            .UpdateAsync(
                Arg.Is<ProductCategory>(x => x.Description == request.Description),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenNothingWasFound()
    {
        // Arrange
        var request = new UpdateProductCategoryRequest
        {
            Id = Guid.NewGuid(),
            Description = "New Product Description"
        };

        repository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(default(ProductCategory));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.Id));

        await repository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<ProductCategory>(),
                Arg.Any<CancellationToken>());
    }
}
