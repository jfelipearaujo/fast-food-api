using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductCategoryAggregate;

using Infrastructure.Repositories;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data.Common;

using Utils.Tests.Builders.Domain.Entities;

namespace Infrastructure.Tests.Repositories;

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

        productCategory = new ProductCategoryBuilder().WithSample().Build();

        dbContext.ProductCategory.Add(productCategory);
        dbContext.SaveChanges();

        sut = new ProductRepository(dbContext);
    }

    [Fact]
    public async Task ShouldCreateProductSuccessfully()
    {
        // Arrange
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

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
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        await sut.CreateAsync(product, cancellationToken: default);

        // Act
        var response = await sut.DeleteAsync(product, cancellationToken: default);

        // Assert
        response.Should().Be(1);
        dbContext.Product.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldGetAllProductSuccessfully()
    {
        // Arrange
        var firstProduct = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        var secondProduct = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

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
    public async Task ShouldGetAllProductByCategorySuccessfully()
    {
        // Arrange
        var firstProduct = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        var secondProduct = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        await sut.CreateAsync(firstProduct, cancellationToken: default);
        await sut.CreateAsync(secondProduct, cancellationToken: default);

        // Act
        var response = await sut.GetAllByCategoryAsync(productCategory.Description, cancellationToken: default);

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
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

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
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        // Act
        var response = await sut.GetByIdAsync(product.Id, cancellationToken: default);

        // Assert
        response.Should().BeNull();
    }

    [Fact]
    public async Task ShouldUpdateProductSuccessfully()
    {
        // Arrange
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        await sut.CreateAsync(product, cancellationToken: default);

        product.Update("New Description", product.Price, product.ImageUrl);

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
