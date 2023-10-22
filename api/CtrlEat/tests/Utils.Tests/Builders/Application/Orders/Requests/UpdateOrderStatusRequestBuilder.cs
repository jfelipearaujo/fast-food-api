using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.UpdateOrderStatus.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class UpdateOrderStatusRequestBuilder
{
    private Guid orderId;
    private string status;

    public UpdateOrderStatusRequestBuilder()
    {
        Reset();
    }

    public UpdateOrderStatusRequestBuilder Reset()
    {
        orderId = default;
        status = default;
        return this;
    }

    public UpdateOrderStatusRequestBuilder WithSample()
    {
        WithOrderId(Guid.NewGuid());
        WithStatus("Created");
        return this;
    }

    public UpdateOrderStatusRequestBuilder WithOrderId(OrderId orderId)
    {
        this.orderId = orderId.Value;
        return this;
    }

    public UpdateOrderStatusRequestBuilder WithOrderId(Guid orderId)
    {
        this.orderId = orderId;
        return this;
    }

    public UpdateOrderStatusRequestBuilder WithStatus(OrderStatus status)
    {
        this.status = status.ToString();
        return this;
    }

    public UpdateOrderStatusRequestBuilder WithStatus(string status)
    {
        this.status = status;
        return this;
    }

    public UpdateOrderStatusRequest Build()
    {
        return new()
        {
            OrderId = orderId,
            Status = status
        };
    }
}
