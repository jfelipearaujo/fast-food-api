using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.GetOrderById.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class GetOrderByIdRequestBuilder
{
    private Guid orderId;

    public GetOrderByIdRequestBuilder()
    {
        Reset();
    }

    public GetOrderByIdRequestBuilder Reset()
    {
        orderId = default;
        return this;
    }

    public GetOrderByIdRequestBuilder WithSample()
    {
        WithOrderId(Guid.NewGuid());
        return this;
    }

    public GetOrderByIdRequestBuilder WithOrderId(OrderId orderId)
    {
        this.orderId = orderId.Value;
        return this;
    }

    public GetOrderByIdRequestBuilder WithOrderId(Guid orderId)
    {
        this.orderId = orderId;
        return this;
    }

    public GetOrderByIdRequest Build()
    {
        return new GetOrderByIdRequest
        {
            OrderId = orderId
        };
    }
}