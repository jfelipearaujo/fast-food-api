using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Application.Tests.UseCases.Products
{
    public class GetProductsByCategoryUseCaseTests
    {
        private readonly GetProductsByCategoryUseCase sut;

        private readonly IProductRepository repository;

        public GetProductsByCategoryUseCaseTests()
        {
            repository = Substitute.For<IProductRepository>();

            sut = new GetProductsByCategoryUseCase(repository);
        }

        [Fact]
        public async Task ShouldGetProductsByCategorySuccessfully()
        {
            // Arrange
            var request = new GetProductsByCategoryRequest
            {
                Category = "category"
            };

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
                .GetAllByCategoryAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Product>
                {
                    productOne,
                    productTwo
                });

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

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
    }
}
