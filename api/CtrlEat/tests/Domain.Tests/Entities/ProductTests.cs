using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using Utils.Tests.Builders.Domain.Entities;

namespace Domain.Tests.Entities;

public class ProductTests
{
    [Fact]
    public void ShouldCreateValidProduct()
    {
        // Arrange
        var description = "Product description";
        var price = Money.Create(10, Money.BRL).Value;
        var imageUrl = "https://image.com";
        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        // Act
        var result = Product.Create(description, price, imageUrl, productCategory);

        // Assert
        result.Should().BeSuccess();
    }

    [Fact]
    public void ShouldUpdateProductInformation()
    {
        // Arrange
        var description = "Product description";
        var price = Money.Create(10, Money.BRL).Value;
        var imageUrl = "https://image.com";
        var productCategory = new ProductCategoryBuilder()
            .WithSample()
            .Build();

        var product = Product.Create(description, price, imageUrl, productCategory).Value;

        // Act
        product.Update("New description",
            Money.Create(20, Money.BRL).Value,
            "https://new-image.com");

        // Assert
        product.Description.Should().Be("New description");
        product.Price.Should().Be(Money.Create(20, Money.BRL).Value);
        product.ImageUrl.Should().Be("https://new-image.com");
    }
}
