using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories.Responses;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Application.Tests.UseCases.Products
{
    public class CreateProductUseCaseTests
    {
        private readonly CreateProductUseCase sut;

        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public CreateProductUseCaseTests()
        {
            productRepository = Substitute.For<IProductRepository>();
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();


            sut = new CreateProductUseCase(productRepository, productCategoryRepository);
        }

        [Fact]
        public async Task ShouldCreateProductSuccessfully()
        {
            // Arrange
            var request = new CreateProductRequest
            {
                ProductCategoryId = Guid.NewGuid(),
                Description = "Product",
                UnitPrice = 1.0m,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory
                {
                    Id = request.ProductCategoryId,
                    Description = "Product Category"
                });

            var expectedResponse = new ProductResponse
            {
                ProductCategory = new ProductCategoryResponse
                {
                    Id = request.ProductCategoryId,
                    Description = "Product Category"
                },
                Description = "Product",
                UnitPrice = 1.0m,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
                result.Value.Id.Should().NotBeEmpty();
            });

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductCategoryId),
                    Arg.Any<CancellationToken>());

            await productRepository
                .Received(1)
                .CreateAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleIfProductCategoryWasNotFound()
        {
            // Arrange
            var request = new CreateProductRequest
            {
                ProductCategoryId = Guid.NewGuid(),
                Description = "Product",
                UnitPrice = 1.0m,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productCategoryRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.ProductCategoryId));

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductCategoryId),
                    Arg.Any<CancellationToken>());

            await productRepository
                .DidNotReceive()
                .CreateAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
