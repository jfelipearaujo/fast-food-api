using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.Products;
using Domain.UseCases.Products.Requests;

namespace Application.Tests.UseCases.Products
{
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

            repository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new Product
                {
                    Id = request.Id,
                    Description = "Product"
                });

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
                    Arg.Is<Guid>(x => x == request.Id),
                    Arg.Any<CancellationToken>());

            await repository
                .Received(1)
                .DeleteAsync(
                    Arg.Is<Product>(x => x.Id == request.Id),
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
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(Product));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.Id));

            await repository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.Id),
                    Arg.Any<CancellationToken>());

            await repository
                .DidNotReceive()
                .DeleteAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
