using Domain.Models;

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

        private readonly AppDbContext _dbContext;

        private readonly DbConnection _dbConnection;

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public ProductCategoryRepositoryTests()
        {
            _dbConnection = new SqliteConnection("Filename=:memory:");
            _dbConnection.Open();

            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_dbConnection)
                .Options;

            _dbContext = new AppDbContext(_dbContextOptions);

            _dbContext.Database.Migrate();

            sut = new ProductCategoryRepository(_dbContext);
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
            var result = await sut.CreateAsync(category, cancellationToken: default);

            // Assert
            result.Should().Be(1);
            _dbContext.ProductCategory.Should().NotBeNullOrEmpty();

            var categoryOnDb = await _dbContext.ProductCategory.FindAsync(category.Id);

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
            var result = await sut.DeleteAsync(category.Id, cancellationToken: default);

            // Assert
            result.Should().Be(1);
            _dbContext.ProductCategory.Should().BeNullOrEmpty();
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
            var result = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            var expectedResult = new List<ProductCategory>
            {
                firstCategory,
                secondCategory
            };

            result
                .Should()
                .NotBeNullOrEmpty()
                .And.BeEquivalentTo(expectedResult);

            _dbContext.ProductCategory
                .Count()
                .Should()
                .Be(2);
        }

        [Fact]
        public async Task ShouldGetAllProductCategorySuccessfullyWhenThereIsNoData()
        {
            // Arrange

            // Act
            var result = await sut.GetAllAsync(cancellationToken: default);

            // Assert
            result
                .Should()
                .BeNullOrEmpty();
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
            var result = await sut.GetByIdAsync(category.Id, cancellationToken: default);

            // Assert
            result.Should().BeEquivalentTo(category);
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
            var result = await sut.GetByIdAsync(category.Id, cancellationToken: default);

            // Assert
            result.Should().BeNull();
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
            var result = await sut.UpdateAsync(category, cancellationToken: default);

            // Assert
            result.Should().Be(1);

            var categoryOnDb = await _dbContext.ProductCategory.FindAsync(category.Id);

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
                _dbConnection.Dispose();
                _dbContext.Dispose();
            }
        }
    }
}