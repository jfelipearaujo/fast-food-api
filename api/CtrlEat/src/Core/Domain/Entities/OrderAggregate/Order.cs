using Domain.Common.Models;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using FluentResults;

namespace Domain.Entities.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public OrderStatus Status { get; private set; }

    public ClientId ClientId { get; set; }

    public Client Client { get; set; }

    public ICollection<OrderItem> Items { get; set; }

    private Order()
    {
    }

    private Order(
        Client client,
        OrderId? orderId = null)
        : base(orderId ?? OrderId.CreateUnique())
    {
        Status = OrderStatus.Created;
        Client = client;
    }

    public void AddItem(OrderItem item)
    {
        Items ??= new List<OrderItem>();

        Items.Add(item);
    }

    public static Result<Order> Create(
        Client client,
        OrderId? orderId = null)
    {
        return new Order(client, orderId);
    }
}
