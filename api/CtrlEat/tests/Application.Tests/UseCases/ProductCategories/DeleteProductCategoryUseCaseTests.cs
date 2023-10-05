using Application.UseCases.Common.Errors;
using Application.UseCases.ProductCategories.DeleteProductCategory;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.ProductCategories;

public class DeleteProductCategoryUseCaseTests
{
    private readonly DeleteProductCategoryUseCase sut;

    private readonly IProductCategoryRepository repository;

    public DeleteProductCategoryUseCaseTests()
    {
        repository = Substitute.For<IProductCategoryRepository>();

        sut = new DeleteProductCategoryUseCase(repository);
    }

    [Fact]
    public async Task ShouldDeleteProductCategorySuccessfully()
    {
        // Arrange
        var request = new DeleteProductCategoryRequest
        {
            Id = Guid.NewGuid(),
        };

        repository
            .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
            .Returns(new ProductCategoryBuilder().WithSample().Build());

        repository
            .DeleteAsync(Arg.Any<ProductCategory>(), Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.HaveValue(1);

        await repository
            .Received(1)
            .DeleteAsync(
                Arg.Any<ProductCategory>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnNullIfNothingWasFound()
    {
        // Arrange
        var request = new DeleteProductCategoryRequest
        {
            Id = Guid.NewGuid(),
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
            .DeleteAsync(
                Arg.Any<ProductCategory>(),
                Arg.Any<CancellationToken>());
    }
}
