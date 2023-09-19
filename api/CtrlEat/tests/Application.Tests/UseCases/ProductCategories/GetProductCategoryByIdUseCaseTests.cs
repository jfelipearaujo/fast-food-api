using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Application.Tests.UseCases.ProductCategories
{
    public class GetProductCategoryByIdUseCaseTests
    {
        private readonly GetProductCategoryByIdUseCase sut;

        private readonly IProductCategoryRepository repository;

        public GetProductCategoryByIdUseCaseTests()
        {
            repository = Substitute.For<IProductCategoryRepository>();

            sut = new GetProductCategoryByIdUseCase(repository);
        }

        [Fact]
        public async Task ShouldGetProductCategorySuccessfully()
        {
            // Arrange
            var request = new GetProductCategoryByIdRequest
            {
                Id = Guid.NewGuid()
            };

            repository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory
                {
                    Id = request.Id,
                    Description = "Product Category"
                });

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(new ProductCategoryResponse
                {
                    Id = request.Id,
                    Description = "Product Category"
                });
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

            repository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.Id));
        }
    }
}
