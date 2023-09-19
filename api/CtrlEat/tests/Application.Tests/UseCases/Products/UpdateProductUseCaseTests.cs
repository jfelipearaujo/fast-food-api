using Application.UseCases.Products;

using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.ProductCategories;
using Domain.Errors.Products;
using Domain.UseCases.ProductCategories.Responses;
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
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new Product
                {
                    Id = request.ProductId,
                    ProductCategoryId = request.ProductCategoryId,
                    ProductCategory = new ProductCategory
                    {
                        Id = request.ProductCategoryId,
                        Description = "Product Category"
                    },
                    Description = "Old Product Description",
                    UnitPrice = 10,
                    Currency = "BRL",
                    ImageUrl = "http://image.com/123.png"
                });

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(new ProductResponse
                {
                    Id = request.ProductId,
                    ProductCategory = new ProductCategoryResponse
                    {
                        Id = request.ProductCategoryId,
                        Description = "Product Category"
                    },
                    Description = "New Product Description",
                    UnitPrice = 10,
                    Currency = "BRL",
                    ImageUrl = "http://image.com/123.png"
                });
            });

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .DidNotReceive()
                .GetByIdAsync(
                    Arg.Any<Guid>(),
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
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new Product
                {
                    Id = request.ProductId,
                    ProductCategoryId = Guid.NewGuid(),
                    ProductCategory = new ProductCategory
                    {
                        Id = Guid.NewGuid(),
                        Description = "Old Product Category"
                    },
                    Description = "Product Description",
                    UnitPrice = 10,
                    Currency = "BRL",
                    ImageUrl = "http://image.com/123.png"
                });

            productCategoryRepository
                .GetByIdAsync(Arg.Is<Guid>(x => x == newProductCategoryId), Arg.Any<CancellationToken>())
                .Returns(new ProductCategory
                {
                    Id = newProductCategoryId,
                    Description = "New Product Category"
                });

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(new ProductResponse
                {
                    Id = request.ProductId,
                    ProductCategory = new ProductCategoryResponse
                    {
                        Id = newProductCategoryId,
                        Description = "New Product Category"
                    },
                    Description = "Product Description",
                    UnitPrice = 10,
                    Currency = "BRL",
                    ImageUrl = "http://image.com/123.png"
                });
            });

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == newProductCategoryId),
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
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(default(Product));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductNotFoundError(request.ProductId));

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .DidNotReceive()
                .GetByIdAsync(
                    Arg.Any<Guid>(),
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
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            productRepository
                .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(new Product
                {
                    Id = request.ProductId,
                    ProductCategoryId = Guid.NewGuid(),
                    ProductCategory = new ProductCategory
                    {
                        Id = Guid.NewGuid(),
                        Description = "Old Product Category"
                    },
                    Description = "Product Description",
                    UnitPrice = 10,
                    Currency = "BRL",
                    ImageUrl = "http://image.com/123.png"
                });

            productCategoryRepository
                .GetByIdAsync(Arg.Is<Guid>(x => x == newProductCategoryId), Arg.Any<CancellationToken>())
                .Returns(default(ProductCategory));

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ProductCategoryNotFoundError(request.ProductCategoryId));

            await productRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == request.ProductId),
                    Arg.Any<CancellationToken>());

            await productCategoryRepository
                .Received(1)
                .GetByIdAsync(
                    Arg.Is<Guid>(x => x == newProductCategoryId),
                    Arg.Any<CancellationToken>());

            await productRepository
                .DidNotReceive()
                .UpdateAsync(
                    Arg.Any<Product>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
