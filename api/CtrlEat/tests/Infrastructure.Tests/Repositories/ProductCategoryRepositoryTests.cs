using Domain.Entities;

using Infrastructure.Repositories;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data.Common;

namespace Infrastructure.Tests.Repositories
{
    public class ProductCategoryRepositoryTests : IDisposable
    {
        private readonly ProductCategoryRepository sut;

        private readonly AppDbContext dbContext;

        private readonly DbConnection dbConnection;

        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public ProductCategoryRepositoryTests()
        {
            dbConnection = new SqliteConnection("Filename=:memory:");
            dbConnection.Open();

            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(dbConnection)
                .Options;

            dbContext = new AppDbContext(dbContextOptions);

            dbContext.Database.Migrate();

            sut = new ProductCategoryRepository(dbContext);
        }

        [Fact]
        public async Task ShouldCreateProductCategorySuccessfully()
        {
            // Arrange
            var category = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category"
            };

            // Act
            var response = await sut.CreateAsync(category, cancellationToken: default);

            // Assert
            response.Should().Be(1);
            dbContext.ProductCategory.Should().NotBeNullOrEmpty();

            var categoryOnDb = await dbContext.ProductCategory.FindAsync(category.Id);

            categoryOnDb.Should().NotBeNull().And.BeEquivalentTo(category);
        }

        [Fact]
        public async Task ShouldDeleteProductCategorySuccessfully()
        {
            // Arrange
            var category = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category"
            };

            await sut.CreateAsync(category, cancellationToken: default);

            // Act
            var response = await sut.DeleteAsync(category.Id, cancellationToken: default);

            // Assert
            response.Should().Be(1);
            dbContext.ProductCategory.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetAllProductCategorySuccessfully()
        {
            // Arrange
            var firstCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category #1"
            };

            var secondCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category #2"
            };

            await sut.CreateAsync(firstCategory, cancellationToken: default);
            await sut.CreateAsync(secondCategory, cancellationToken: default);

            // Act
            var response = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            response.Should().NotBeNullOrEmpty().And.BeEquivalentTo(new List<ProductCategory>
            {
                firstCategory,
                secondCategory
            });

            dbContext.ProductCategory.Count().Should().Be(2);
        }

        [Fact]
        public async Task ShouldGetAllProductCategorySuccessfullyWhenThereIsNoData()
        {
            // Arrange

            // Act
            var response = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            response.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetByIdProductCategorySuccessfully()
        {
            // Arrange
            var category = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category"
            };

            await sut.CreateAsync(category, cancellationToken: default);

            // Act
            var response = await sut.GetByIdAsync(category.Id, cancellationToken: default);

            // Assert
            response.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task ShouldGetByIdProductCategorySuccessfullyWhenThereIsNoData()
        {
            // Arrange
            var category = new ProductCategory
            {
                Id = Guid.NewGuid(),
            };

            // Act
            var response = await sut.GetByIdAsync(category.Id, cancellationToken: default);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task ShouldUpdateProductCategorySuccessfully()
        {
            // Arrange
            var category = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category"
            };

            await sut.CreateAsync(category, cancellationToken: default);

            category.Description = "New Description";

            // Act
            var response = await sut.UpdateAsync(category, cancellationToken: default);

            // Assert
            response.Should().Be(1);

            var categoryOnDb = await dbContext.ProductCategory.FindAsync(category.Id);

            categoryOnDb.Should().NotBeNull().And.BeEquivalentTo(category);
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