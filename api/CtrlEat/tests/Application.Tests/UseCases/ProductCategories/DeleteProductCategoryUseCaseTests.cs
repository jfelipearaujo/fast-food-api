using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

namespace Application.Tests.UseCases.ProductCategories
{
    public class DeleteProductCategoryUseCaseTests
    {
        private readonly DeleteProductCategoryUseCase sut;

        private readonly IProductCategoryRepository repository;

        public DeleteProductCategoryUseCaseTests()
        {
            repository = Substitute.For<IProductCategoryRepository>();

            sut = new DeleteProductCategoryUseCase(repository);
        }

        [Fact]
        public async Task ShouldDeleteProductCategorySuccessfully()
        {
            // Arrange
            var request = new DeleteProductCategoryRequest
            {
                Id = Guid.NewGuid(),
            };

            repository
                .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory());

            repository
                .DeleteAsync(Arg.Any<ProductCategory>(), Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.HaveValue(1);

            await repository
                .Received(1)
                .DeleteAsync(
                    Arg.Any<ProductCategory>(),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldReturnNullIfNothingWasFound()
        {
            // Arrange
            var request = new DeleteProductCategoryRequest
            {
                Id = Guid.NewGuid(),
            };

            repository
                .GetByIdAsync(Arg.Any<ProductCategoryId>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.Id));

            await repository
                .DidNotReceive()
                .DeleteAsync(
                    Arg.Any<ProductCategory>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
