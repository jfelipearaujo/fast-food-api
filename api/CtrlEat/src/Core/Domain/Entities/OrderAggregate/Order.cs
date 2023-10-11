using Domain.Common.Models;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using FluentResults;

namespace Domain.Entities.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public TrackId TrackId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime StatusUpdatedAt { get; set; }

    public ClientId ClientId { get; set; }

    public Client Client { get; set; }

    public ICollection<OrderItem> Items { get; set; }

    private Order()
    {
    }

    private Order(
        TrackId trackId,
        Client client,
        OrderStatus? status = null,
        DateTime? statusUpdatedAt = null,
        OrderId? orderId = null)
        : base(orderId ?? OrderId.CreateUnique())
    {
        TrackId = trackId;
        Status = status ?? OrderStatus.Created;
        StatusUpdatedAt = statusUpdatedAt ?? DateTime.UtcNow;
        Client = client;
    }

    public void AddItem(OrderItem item)
    {
        Items ??= new List<OrderItem>();

        Items.Add(item);
    }

    public static Result<Order> Create(
        TrackId trackId,
        Client client,
        OrderStatus? status = null,
        DateTime? statusUpdatedAt = null,
        OrderId? orderId = null)
    {
        return new Order(trackId, client, status, statusUpdatedAt, orderId);
    }
}
