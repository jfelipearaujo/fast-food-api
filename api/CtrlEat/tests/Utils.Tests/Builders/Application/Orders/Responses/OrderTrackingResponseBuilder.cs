using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.Common.Responses;

namespace Utils.Tests.Builders.Application.Orders.Responses;

public class OrderTrackingResponseBuilder
{
    private OrderStatus status;
    private List<OrderTrackingDataResponse> orders;

    public OrderTrackingResponseBuilder()
    {
        Reset();
    }

    public OrderTrackingResponseBuilder Reset()
    {
        status = default;
        orders = new List<OrderTrackingDataResponse>();
        return this;
    }

    public OrderTrackingResponseBuilder WithStatus(OrderStatus status)
    {
        this.status = status;
        return this;
    }

    public OrderTrackingResponseBuilder WithOrders(List<Order> orders)
    {
        orders.ForEach(x => this.orders.Add(OrderTrackingDataResponse.MapFromDomain(x)));

        return this;
    }

    public OrderTrackingResponseBuilder WithOrders(List<OrderTrackingDataResponse> orders)
    {
        this.orders = orders;
        return this;
    }

    public OrderTrackingResponse Build()
    {
        return new()
        {
            Status = status,
            Orders = orders
        };
    }
}
