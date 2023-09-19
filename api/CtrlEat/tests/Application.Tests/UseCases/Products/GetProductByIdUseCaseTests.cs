using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Application.Tests.UseCases.Products
{
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

            repository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new Product
                {
                    Id = request.Id,
                    Description = "Product"
                });

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(new ProductResponse
                {
                    Id = request.Id,
                    Description = "Product"
                });
            });

            await repository
                .Received(1)
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
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
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(Product));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.Id));

            await repository
                .Received(1)
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }
    }
}
