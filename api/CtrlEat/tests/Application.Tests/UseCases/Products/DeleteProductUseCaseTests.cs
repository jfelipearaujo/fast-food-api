using Application.UseCases.Products.Common.Errors;
using Application.UseCases.Products.DeleteProduct;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class DeleteProductUseCaseTests
{
    private readonly DeleteProductUseCase sut;

    private readonly IProductRepository repository;

    public DeleteProductUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();

        sut = new DeleteProductUseCase(repository);
    }

    [Fact]
    public async Task ShouldDeleteProductSuccessfully()
    {
        // Arrange
        var request = new DeleteProductRequest
        {
            Id = Guid.NewGuid(),
        };

        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithId(request.Id)
            .WithProductCategory(productCategory)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        repository
            .DeleteAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.HaveValue(1);

        await repository
            .Received(1)
            .GetByIdAsync(
                Arg.Is<ProductId>(x => x.Value == request.Id),
                Arg.Any<CancellationToken>());

        await repository
            .Received(1)
            .DeleteAsync(
                Arg.Is<Product>(x => x.Id.Value == request.Id),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenProductWasNotFound()
    {
        // Arrange
        var request = new DeleteProductRequest
        {
            Id = Guid.NewGuid(),
        };

        repository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(default(Product));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.Id));

        await repository
            .Received(1)
            .GetByIdAsync(
                Arg.Is<ProductId>(x => x.Value == request.Id),
                Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .DeleteAsync(
                Arg.Any<Product>(),
                Arg.Any<CancellationToken>());
    }
}
