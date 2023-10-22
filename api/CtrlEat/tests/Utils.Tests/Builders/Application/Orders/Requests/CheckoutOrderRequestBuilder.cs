using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.CheckoutOrder.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class CheckoutOrderRequestBuilder
{
    private Guid orderId;

    public CheckoutOrderRequestBuilder()
    {
        Reset();
    }

    public CheckoutOrderRequestBuilder Reset()
    {
        orderId = default;

        return this;
    }

    public CheckoutOrderRequestBuilder WithSample()
    {
        WithOrderId(Guid.NewGuid());

        return this;
    }

    public CheckoutOrderRequestBuilder WithOrderId(OrderId orderId)
    {
        this.orderId = orderId.Value;

        return this;
    }

    public CheckoutOrderRequestBuilder WithOrderId(Guid orderId)
    {
        this.orderId = orderId;

        return this;
    }

    public CheckoutOrderRequest Build()
    {
        return new()
        {
            OrderId = orderId
        };
    }
}
