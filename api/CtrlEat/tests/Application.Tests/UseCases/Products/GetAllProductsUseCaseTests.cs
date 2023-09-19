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
            var productOne = new Product
            {
                Id = Guid.NewGuid(),
                Description = "Product 1"
            };

            var productTwo = new Product
            {
                Id = Guid.NewGuid(),
                Description = "Product 2"
            };

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
                    new ProductResponse
                    {
                        Id = productOne.Id,
                        Description = productOne.Description
                    },
                    new ProductResponse
                    {
                        Id = productTwo.Id,
                        Description = productTwo.Description
                    }
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
