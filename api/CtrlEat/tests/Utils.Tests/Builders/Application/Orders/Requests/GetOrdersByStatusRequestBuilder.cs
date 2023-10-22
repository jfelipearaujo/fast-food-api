using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class GetOrdersByStatusRequestBuilder
{
    private string? status;

    public GetOrdersByStatusRequestBuilder()
    {
        Reset();
    }

    public GetOrdersByStatusRequestBuilder Reset()
    {
        status = default;
        return this;
    }

    public GetOrdersByStatusRequestBuilder WithSample()
    {
        WithStatus(OrderStatus.Created);
        return this;
    }

    public GetOrdersByStatusRequestBuilder WithStatus(OrderStatus status)
    {
        this.status = status.ToString();
        return this;
    }

    public GetOrdersByStatusRequestBuilder WithStatus(string? status)
    {
        this.status = status;
        return this;
    }

    public GetOrdersByStatusRequest Build()
    {
        return new GetOrdersByStatusRequest
        {
            Status = status
        };
    }
}
