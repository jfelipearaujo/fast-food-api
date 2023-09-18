using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Models;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Application.Tests.UseCases.ProductCategories
{
    public class UpdateProductCategoryUseCaseTests
    {
        private readonly UpdateProductCategoryUseCase sut;

        private readonly IProductCategoryRepository productCategoryRepository;

        public UpdateProductCategoryUseCaseTests()
        {
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();

            sut = new UpdateProductCategoryUseCase(productCategoryRepository);
        }

        [Fact]
        public async Task ShouldUpdateProductCategorySuccessfully()
        {
            // Arrange
            var request = new UpdateProductCategoryUseCaseRequest
            {
                Id = Guid.NewGuid(),
                Description = "New Product Description"
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory
                {
                    Id = request.Id,
                    Description = "Old Product Description"
                });

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new UpdateProductCategoryUseCaseResponse
            {
                Id = request.Id,
                Description = request.Description
            });

            await productCategoryRepository
                .Received(1)
                .UpdateAsync(
                    Arg.Is<ProductCategory>(x => x.Description == request.Description),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleWhenNothingWasFound()
        {
            // Arrange
            var request = new UpdateProductCategoryUseCaseRequest
            {
                Id = Guid.NewGuid(),
                Description = "New Product Description"
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().BeNull();
            await productCategoryRepository
                .DidNotReceive()
                .UpdateAsync(
                    Arg.Any<ProductCategory>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
