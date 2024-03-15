using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductImage;
using Application.UseCases.Products.GetProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
using Domain.Adapters.Storage.Responses;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.GetProductImage.Requests;

using System.Net;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class GetProductImageUseCaseTests
{
    private readonly GetProductImageUseCase sut;

    private readonly IProductRepository repository;
    private readonly IStorageService storageService;

    public GetProductImageUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();
        storageService = Substitute.For<IStorageService>();

        sut = new GetProductImageUseCase(repository, storageService);
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

        storageService.DownloadFileAsync(Arg.Any<DownloadObjectRequest>(), Arg.Any<CancellationToken>())
            .Returns(new DownloadObjectResponse
            {
                FileStream = new MemoryStream(),
                StatusCode = (int)HttpStatusCode.OK
            });

        // Act
        var response = await sut.Execute(request, CancellationToken.None);

        // Assert
        response.Should().BeSuccess();

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

        storageService.DownloadFileAsync(Arg.Any<DownloadObjectRequest>(), Arg.Any<CancellationToken>())
            .Returns(new DownloadObjectResponse
            {
                FileStream = new MemoryStream(),
                StatusCode = (int)HttpStatusCode.NotFound
            });

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
