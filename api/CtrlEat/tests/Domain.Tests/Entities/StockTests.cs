using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.Errors;

using Utils.Tests.Builders.Domain.Entities;

namespace Domain.Tests.Entities;

public class StockTests
{
    [Fact]
    public void ShouldCreateValidStock()
    {
        // Arrange
        var quantity = 10;
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        // Act
        var result = Stock.Create(quantity, product);

        // Assert
        result.Should().BeSuccess();
    }

    [Fact]
    public void ShouldNotCreateStockWithInvalidQuantity()
    {
        // Arrange
        var quantity = 0;
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        // Act
        var result = Stock.Create(quantity, product);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new StockInvalidQuantityError());
    }
}
