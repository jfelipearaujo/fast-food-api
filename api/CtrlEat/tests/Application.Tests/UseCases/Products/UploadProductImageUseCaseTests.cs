using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.UploadProductImage;
using Application.UseCases.Products.UploadProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

using Utils.Tests.Builders.Application.Products.Requests;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class UploadProductImageUseCaseTests
{
    private readonly UploadProductImageUseCase sut;
    private readonly IProductRepository repository;
    private readonly IFileSystem fileSystem;

    public UploadProductImageUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();
        fileSystem = Substitute.For<IFileSystem>();

        sut = new UploadProductImageUseCase(repository, fileSystem);
    }

    [Fact]
    public async Task ShouldUploadAnImageSuccessfully()
    {
        // Arrange
        var request = new UploadProductImageRequestBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithId(request.Id)
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        fileSystem.Path
            .GetExtension(Arg.Any<string>())
            .Returns(Images.FILE_EXTENSION_JPG);

        repository.GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        fileSystem.Directory.Exists(Arg.Any<string>())
            .Returns(false);

        var files = new Dictionary<string, MockFileData>
        {
            { "./file.jpg", new MockFileData("Hello World") }
        };

        using var stream = new MockFileSystem(files)
            .FileStream
            .New("./file.jpg", FileMode.Open);

        fileSystem.File.OpenWrite(Arg.Any<string>())
            .Returns(stream);

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().Be(product.Id.Value.ToString());
        });

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await repository
            .Received(1)
            .UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenFileExtensionIsInvalid()
    {
        // Arrange
        var request = new UploadProductImageRequestBuilder()
            .WithSample()
            .Build();

        fileSystem.Path
            .GetExtension(Arg.Any<string>())
            .Returns(".txt");

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ProductImageInvalidExtensionError());

        await repository
            .DidNotReceive()
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenProductIsNotFound()
    {
        // Arrange
        var request = new UploadProductImageRequestBuilder()
            .WithSample()
            .Build();

        fileSystem.Path
            .GetExtension(Arg.Any<string>())
            .Returns(Images.FILE_EXTENSION_JPG);

        repository.GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(default(Product));

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ProductNotFoundError(request.Id));

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
}
