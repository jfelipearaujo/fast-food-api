using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class OrderBuilder
{
    private TrackId trackId;
    private Client client;
    private OrderStatus status;
    private List<OrderItem> items;
    private List<Payment> payments;

    public OrderBuilder()
    {
        Reset();
    }

    public OrderBuilder Reset()
    {
        client = default;
        trackId = default;
        status = default;
        items = new List<OrderItem>();
        payments = new List<Payment>();

        return this;
    }

    public OrderBuilder WithSample()
    {
        WithTrackId(TrackId.CreateUnique());
        WithClient(new ClientBuilder().WithSample().Build());
        WithStatus(OrderStatus.Created);

        return this;
    }

    public OrderBuilder WithClient(Client client)
    {
        this.client = client;

        return this;
    }

    public OrderBuilder WithTrackId(TrackId trackId)
    {
        this.trackId = trackId;

        return this;
    }

    public OrderBuilder WithStatus(OrderStatus status)
    {
        this.status = status;

        return this;
    }

    public OrderBuilder WithItem(OrderItem item)
    {
        items.Add(item);

        return this;
    }

    public OrderBuilder WithPayment(Payment payment)
    {
        payments.Add(payment);

        return this;
    }

    public Order Build()
    {
        var order = Order.Create(trackId, client, status).Value;

        items.ForEach(order.AddItem);
        payments.ForEach(order.AddPayment);

        return order;
    }
}
