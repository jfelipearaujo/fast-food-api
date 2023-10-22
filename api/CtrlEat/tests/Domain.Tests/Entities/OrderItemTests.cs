using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Errors;

using Utils.Tests.Builders.Domain.Entities;

namespace Domain.Tests.Entities;

public class OrderItemTests
{
    [Fact]
    public void ShouldCreateValidOrderItem()
    {
        // Arrange
        var quantity = 1;
        var observation = "This is an observation";
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        // Act
        var result = OrderItem.Create(quantity, observation, product);

        // Assert
        result.Should().BeSuccess();
    }

    [Fact]
    public void ShouldNotCreateOrderItemWithInvalidQuantity()
    {
        // Arrange
        var quantity = 0;
        var observation = "This is an observation";
        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        // Act
        var result = OrderItem.Create(quantity, observation, product);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new OrderItemInvalidQuantityError());
    }
}
