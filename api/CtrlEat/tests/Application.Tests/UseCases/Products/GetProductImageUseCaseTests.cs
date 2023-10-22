using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductImage;
using Application.UseCases.Products.GetProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.GetProductImage.Requests;

using System.IO.Abstractions;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class GetProductImageUseCaseTests
{
    private readonly GetProductImageUseCase sut;

    private readonly IProductRepository repository;
    private readonly IFileSystem fileSystem;

    public GetProductImageUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();
        fileSystem = Substitute.For<IFileSystem>();

        sut = new GetProductImageUseCase(repository, fileSystem);
    }

    [Fact]
    public async Task ShouldReturnProductImageSuccessfully()
    {
        // Arrange
        var request = new GetProductImageRequest
        {
            Id = Guid.NewGuid()
        };

        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(
                new ProductCategoryBuilder()
                    .WithSample()
                    .Build())
            .Build();

        repository.GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        fileSystem.Path
            .Combine(Arg.Any<string>(), Arg.Any<string>())
            .Returns($"{product.Id.Value}.jpg");

        fileSystem.File
            .Exists(Arg.Any<string>())
            .Returns(true);

        // Act
        var response = await sut.Execute(request, CancellationToken.None);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().Be($"{product.Id.Value}.jpg");
        });

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenProductIsNotFound()
    {
        // Arrange
        var request = new GetProductImageRequest
        {
            Id = Guid.NewGuid()
        };

        repository.GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(default(Product));

        // Act
        var response = await sut.Execute(request, CancellationToken.None);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ProductNotFoundError(request.Id));

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenProductImageIsNotFound()
    {
        // Arrange
        var request = new GetProductImageRequest
        {
            Id = Guid.NewGuid()
        };

        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(
                new ProductCategoryBuilder()
                    .WithSample()
                    .Build())
            .Build();

        repository.GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        fileSystem.Path
            .Combine(Arg.Any<string>(), Arg.Any<string>())
            .Returns($"{product.Id.Value}.jpg");

        fileSystem.File
            .Exists(Arg.Any<string>())
            .Returns(false);

        // Act
        var response = await sut.Execute(request, CancellationToken.None);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ProductImageNotFountError(request.Id));

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());
    }
}
