using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Models;
using Domain.UseCases.ProductCategories.Requests;

namespace Application.Tests.UseCases.ProductCategories
{
    public class DeleteProductCategoryUseCaseTests
    {
        private readonly DeleteProductCategoryUseCase sut;

        private readonly IProductCategoryRepository productCategoryRepository;

        public DeleteProductCategoryUseCaseTests()
        {
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();

            sut = new DeleteProductCategoryUseCase(productCategoryRepository);
        }

        [Fact]
        public async Task ShouldDeleteProductCategorySuccessfully()
        {
            // Arrange
            var request = new DeleteProductCategoryUseCaseRequest
            {
                Id = Guid.NewGuid(),
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory());

            productCategoryRepository
                .DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().Be(1);

            await productCategoryRepository
                .Received(1)
                .DeleteAsync(
                    Arg.Any<Guid>(),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldReturnNullIfNothingWasFound()
        {
            // Arrange
            var request = new DeleteProductCategoryUseCaseRequest
            {
                Id = Guid.NewGuid(),
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            productCategoryRepository
                .DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().BeNull();

            await productCategoryRepository
                .DidNotReceive()
                .DeleteAsync(
                    Arg.Any<Guid>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
