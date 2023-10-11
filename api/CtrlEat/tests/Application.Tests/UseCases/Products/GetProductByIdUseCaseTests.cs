using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductById;

using Domain.Adapters;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetProductById.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Products;

public class GetProductByIdUseCaseTests
{
    private readonly GetProductByIdUseCase sut;

    private readonly IProductRepository repository;

    public GetProductByIdUseCaseTests()
    {
        repository = Substitute.For<IProductRepository>();

        sut = new GetProductByIdUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetProductByIdSuccessfully()
    {
        // Arrange
        var request = new GetProductByIdRequest
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

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(ProductResponse.MapFromDomain(product));
        });

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenProductWasNotFound()
    {
        // Arrange
        var request = new GetProductByIdRequest
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
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());
    }
}
