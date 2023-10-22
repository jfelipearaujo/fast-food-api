using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate;

namespace Utils.Tests.Builders.Domain.Entities;

public class OrderItemBuilder
{
    private OrderItemId id;
    private int quantity;
    private string observation;
    private Product product;

    public OrderItemBuilder()
    {
        Reset();
    }

    public OrderItemBuilder Reset()
    {
        id = default;
        quantity = default;
        observation = default;
        product = default;

        return this;
    }

    public OrderItemBuilder WithSample()
    {
        id = OrderItemId.CreateUnique();
        quantity = 1;
        observation = "This is an observation";
        product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        return this;
    }

    public OrderItem Build()
    {
        return OrderItem.Create(quantity, observation, product, id).Value;
    }
}
