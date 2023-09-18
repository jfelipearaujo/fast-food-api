using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Entities;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Application.Tests.UseCases.ProductCategories
{
    public class GetProductCategoryUseCaseTests
    {
        private readonly GetProductCategoryByIdUseCase sut;

        private readonly IProductCategoryRepository productCategoryRepository;

        public GetProductCategoryUseCaseTests()
        {
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();

            sut = new GetProductCategoryByIdUseCase(productCategoryRepository);
        }

        [Fact]
        public async Task ShouldGetProductCategorySuccessfully()
        {
            // Arrange
            var request = new GetProductCategoryByIdRequest
            {
                Id = Guid.NewGuid()
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory
                {
                    Id = request.Id,
                    Description = "Product Category"
                });

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new ProductCategoryResponse
            {
                Id = request.Id,
                Description = "Product Category"
            });
        }

        [Fact]
        public async Task ShouldHandleWhenNothingWasFound()
        {
            // Arrange
            var request = new GetProductCategoryByIdRequest
            {
                Id = Guid.NewGuid()
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var result = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            result.Should().BeNull();
        }
    }
}
