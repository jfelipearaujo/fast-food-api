using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Orders.AddOrderItem.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class AddOrderItemRequestBuilder
{
    private Guid orderId;
    private Guid productId;
    private int quantity;
    private string? observation;

    public AddOrderItemRequestBuilder()
    {
        Reset();
    }

    public AddOrderItemRequestBuilder Reset()
    {
        orderId = default;
        productId = default;
        quantity = default;
        observation = default;

        return this;
    }

    public AddOrderItemRequestBuilder WithSample()
    {
        WithOrderId(Guid.NewGuid());
        WithProductId(Guid.NewGuid());
        WithQuantity(1);
        WithObservation("Observation");

        return this;
    }

    public AddOrderItemRequestBuilder WithOrderId(OrderId orderId)
    {
        this.orderId = orderId.Value;
        return this;
    }

    public AddOrderItemRequestBuilder WithOrderId(Guid orderId)
    {
        this.orderId = orderId;
        return this;
    }

    public AddOrderItemRequestBuilder WithProductId(ProductId productId)
    {
        this.productId = productId.Value;
        return this;
    }

    public AddOrderItemRequestBuilder WithProductId(Guid productId)
    {
        this.productId = productId;
        return this;
    }

    public AddOrderItemRequestBuilder WithQuantity(int quantity)
    {
        this.quantity = quantity;
        return this;
    }

    public AddOrderItemRequestBuilder WithObservation(string? observation)
    {
        this.observation = observation;
        return this;
    }

    public AddOrderItemRequest Build()
    {
        return new()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Observation = observation
        };
    }
}
