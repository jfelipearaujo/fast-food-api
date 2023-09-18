using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Models;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

namespace Application.Tests.UseCases.ProductCategories
{
    public class CreateProductCategoryUseCaseTests
    {
        private readonly CreateProductCategoryUseCase sut;

        private readonly IProductCategoryRepository productCategoryRepository;

        public CreateProductCategoryUseCaseTests()
        {
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();

            sut = new CreateProductCategoryUseCase(productCategoryRepository);
        }

        [Fact]
        public async Task ShouldCreateProductCategorySuccessfully()
        {
            // Arrange
            var request = new CreateProductCategoryUseCaseRequest
            {
                Description = "Product Category Description",
            };

            var expectedResponse = new CreateProductCategoryUseCaseResponse
            {
                Description = "Product Category Description",
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
            response.Id.Should().NotBeEmpty();

            await productCategoryRepository
                .Received(1)
                .CreateAsync(
                    Arg.Any<ProductCategory>(),
                    Arg.Any<CancellationToken>());
        }
    }
}