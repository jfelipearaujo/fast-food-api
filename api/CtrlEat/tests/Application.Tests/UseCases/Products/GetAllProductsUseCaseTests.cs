using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.UseCases.Products.Responses;

namespace Application.Tests.UseCases.Products
{
    public class GetAllProductsUseCaseTests
    {
        private readonly GetAllProductsUseCase sut;

        private readonly IProductRepository repository;

        public GetAllProductsUseCaseTests()
        {
            repository = Substitute.For<IProductRepository>();

            sut = new GetAllProductsUseCase(repository);
        }

        [Fact]
        public async Task ShouldGetAllProductsSuccessfully()
        {
            // Arrange
            var productOne = new ProductBuilder().WithSample().Build();
            var productTwo = new ProductBuilder().WithSample().Build();

            repository
                .GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(new List<Product>
                {
                    productOne,
                    productTwo
                });

            // Act
            var response = await sut.ExecuteAsync(cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(new List<ProductResponse>
                {
                    ProductResponse.MapFromDomain(productOne),
                    ProductResponse.MapFromDomain(productTwo),
                });
            });
        }

        [Fact]
        public async Task ShouldReturnNothingIfNothingWasFound()
        {
            // Arrange
            repository
                .GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(new List<Product>());

            // Act
            var response = await sut.ExecuteAsync(cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(Enumerable.Empty<ProductResponse>());
            });
        }
    }
}
