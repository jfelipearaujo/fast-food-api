using Domain.Entities;

using Infrastructure.Repositories;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data.Common;

namespace Infrastructure.Tests.Repositories
{
    public class ProductRepositoryTests : IDisposable
    {
        private readonly ProductRepository sut;

        private readonly AppDbContext dbContext;

        private readonly DbConnection dbConnection;

        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        private readonly ProductCategory productCategory;

        public ProductRepositoryTests()
        {
            dbConnection = new SqliteConnection("Filename=:memory:");
            dbConnection.Open();

            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(dbConnection)
                .Options;

            dbContext = new AppDbContext(dbContextOptions);

            dbContext.Database.Migrate();

            productCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category"
            };

            dbContext.ProductCategory.Add(productCategory);
            dbContext.SaveChanges();

            sut = new ProductRepository(dbContext);
        }

        [Fact]
        public async Task ShouldCreateProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            // Act
            var response = await sut.CreateAsync(product, cancellationToken: default);

            // Assert
            response.Should().Be(1);
            dbContext.Product.Should().NotBeNullOrEmpty();

            var productOnDb = await dbContext.Product.FindAsync(product.Id);

            productOnDb.Should().NotBeNull().And.BeEquivalentTo(product);
        }

        [Fact]
        public async Task ShouldDeleteProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            await sut.CreateAsync(product, cancellationToken: default);

            // Act
            var response = await sut.DeleteAsync(product.Id, cancellationToken: default);

            // Assert
            response.Should().Be(1);
            dbContext.Product.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetAllProductSuccessfully()
        {
            // Arrange
            var firstProduct = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product 1",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            var secondProduct = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product 2",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            await sut.CreateAsync(firstProduct, cancellationToken: default);
            await sut.CreateAsync(secondProduct, cancellationToken: default);

            // Act
            var response = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            response.Should().NotBeNullOrEmpty().And.BeEquivalentTo(new List<Product>
            {
                firstProduct,
                secondProduct
            });

            dbContext.Product.Count().Should().Be(2);
        }

        [Fact]
        public async Task ShouldGetAllProductSuccessfullyWhenThereIsNoData()
        {
            // Arrange

            // Act
            var response = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            response.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetByIdProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            await sut.CreateAsync(product, cancellationToken: default);

            // Act
            var response = await sut.GetByIdAsync(product.Id, cancellationToken: default);

            // Assert
            response.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task ShouldGetByIdProductSuccessfullyWhenThereIsNoData()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            // Act
            var response = await sut.GetByIdAsync(product.Id, cancellationToken: default);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task ShouldUpdateProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategory.Id,
                Description = "Product",
                UnitPrice = 10,
                Currency = "BRL",
                ImageUrl = "http://image.com/123.png"
            };

            await sut.CreateAsync(product, cancellationToken: default);

            product.Description = "New Description";

            // Act
            var response = await sut.UpdateAsync(product, cancellationToken: default);

            // Assert
            response.Should().Be(1);

            var productOnDb = await dbContext.Product.FindAsync(product.Id);

            productOnDb.Should().NotBeNull().And.BeEquivalentTo(product);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbConnection.Dispose();
                dbContext.Dispose();
            }
        }
    }
}
