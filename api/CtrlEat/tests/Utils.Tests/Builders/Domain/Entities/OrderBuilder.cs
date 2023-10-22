using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class OrderBuilder
{
    private TrackId trackId;
    private Client client;

    public OrderBuilder()
    {
        Reset();
    }

    public OrderBuilder Reset()
    {
        client = default;
        trackId = default;

        return this;
    }

    public OrderBuilder WithSample()
    {
        trackId = TrackId.CreateUnique();
        client = new ClientBuilder().WithSample().Build();

        return this;
    }

    public OrderBuilder WithClient(Client client)
    {
        this.client = client;

        return this;
    }

    public Order Build()
    {
        return Order.Create(trackId, client).Value;
    }
}
