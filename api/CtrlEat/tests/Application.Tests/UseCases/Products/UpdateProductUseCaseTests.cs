using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;
using Domain.Errors.ProductCategories;
using Domain.Errors.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

namespace Application.Tests.UseCases.Products
{
    public class UpdateProductUseCaseTests
    {
        private readonly UpdateProductUseCase sut;

        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public UpdateProductUseCaseTests()
        {
            productRepository = Substitute.For<IProductRepository>();
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();


            sut = new UpdateProductUseCase(productRepository, productCategoryRepository);
        }

        [Fact]
        public async Task ShouldUpdateProductDescriptionSuccessfully()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                ProductId = Guid.NewGuid(),
                ProductCategoryId = Guid.NewGuid(),
                Description = "New Product Description",
                Amount = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            var product = new ProductBuilder()
                .WithSample()
                .WithId(request.ProductId)
                .WithProductCategoryId(request.ProductCategoryId)
                .WithDescription("Old Product Description")
                .Build();

            productRepository
                .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
                .Returns(product);

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            var expectedResponse = ProductResponse.MapFromDomain(product);
            expectedResponse.Description = "New Product Description";

            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse);
            });

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductId>(x => x.Value == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .DidNotReceive()
                .GetByIdAsync(
                    Arg.Any<ProductCategoryId>(),
                    Arg.Any<CancellationToken>());

            await productRepository
                .Received(1)
                .UpdateAsync(
                    Arg.Is<Product>(x => x.Description == request.Description),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldUpdateProductCategorySuccessfully()
        {
            // Arrange
            var newProductCategoryId = Guid.NewGuid();

            var request = new UpdateProductRequest
            {
                ProductId = Guid.NewGuid(),
                ProductCategoryId = newProductCategoryId,
                Description = "Product Description",
                Amount = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            var product = new ProductBuilder()
                .WithSample()
                .WithId(request.ProductId)
                .Build();

            productRepository
                .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
                .Returns(product);

            var productCategory = new ProductCategoryBuilder()
                .WithId(newProductCategoryId)
                .WithDescription("New Product Category")
                .Build();

            productCategoryRepository
                .GetByIdAsync(Arg.Is<ProductCategoryId>(x => x.Value == newProductCategoryId), Arg.Any<CancellationToken>())
                .Returns(productCategory);

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            var expectedResponse = ProductResponse.MapFromDomain(product);
            expectedResponse.ProductCategory.Description = "New Product Category";

            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse);
            });

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductId>(x => x.Value == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductCategoryId>(x => x.Value == newProductCategoryId),
                    Arg.Any<CancellationToken>());

            await productRepository
                .Received(1)
                .UpdateAsync(
                    Arg.Is<Product>(x => x.Description == request.Description),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleWhenProductWasNotFound()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                ProductId = Guid.NewGuid(),
                ProductCategoryId = Guid.NewGuid(),
                Description = "New Product Description",
                Amount = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productRepository
                .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
                .Returns(default(Product));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.ProductId));

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductId>(x => x.Value == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .DidNotReceive()
                .GetByIdAsync(
                    Arg.Any<ProductCategoryId>(),
                    Arg.Any<CancellationToken>());

            await productRepository
                .DidNotReceive()
                .UpdateAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleWhenProductCategoryWasNotFound()
        {
            // Arrange
            var newProductCategoryId = Guid.NewGuid();

            var request = new UpdateProductRequest
            {
                ProductId = Guid.NewGuid(),
                ProductCategoryId = newProductCategoryId,
                Description = "Product Description",
                Amount = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            var product = new ProductBuilder()
                .WithSample()
                .WithId(request.ProductId)
                .Build();

            productRepository
                .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
                .Returns(product);

            productCategoryRepository
                .GetByIdAsync(Arg.Is<ProductCategoryId>(x => x.Value == newProductCategoryId), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.ProductCategoryId));

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductId>(x => x.Value == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<ProductCategoryId>(x => x.Value == newProductCategoryId),
                    Arg.Any<CancellationToken>());

            await productRepository
                .DidNotReceive()
                .UpdateAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
