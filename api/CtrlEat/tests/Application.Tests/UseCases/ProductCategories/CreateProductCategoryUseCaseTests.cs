using Application.UseCases.ProductCategories.CreateProductCategory;

using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate;

using Utils.Tests.Builders.Application.ProductCategories.Requests;

namespace Application.Tests.UseCases.ProductCategories;

public class CreateProductCategoryUseCaseTests
{
    private readonly CreateProductCategoryUseCase sut;

    private readonly IProductCategoryRepository repository;

    public CreateProductCategoryUseCaseTests()
    {
        repository = Substitute.For<IProductCategoryRepository>();

        sut = new CreateProductCategoryUseCase(repository);
    }

    [Fact]
    public async Task ShouldCreateProductCategorySuccessfully()
    {
        // Arrange
        var request = new CreateProductCategoryRequestBuilder()
            .WithSample()
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess();

        await repository
            .Received(1)
            .CreateAsync(
                Arg.Any<ProductCategory>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenReceiveInvalidData()
    {
        // Arrange
        var request = new CreateProductCategoryRequestBuilder()
            .WithDescription("")
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure();

        await repository
            .DidNotReceive()
            .CreateAsync(
                Arg.Any<ProductCategory>(),
                Arg.Any<CancellationToken>());
    }
}