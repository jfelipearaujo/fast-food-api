using Domain.Common.Models;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.Errors;
using Domain.Entities.OrderAggregate.ValueObjects;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public TrackId TrackId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime StatusUpdatedAt { get; private set; }

    public ClientId ClientId { get; private set; }

    public Client Client { get; private set; }

    public ICollection<OrderItem> Items { get; private set; }

    public ICollection<Payment> Payments { get; private set; }

    [ExcludeFromCodeCoverage]
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

        Items ??= new List<OrderItem>();
        Payments ??= new List<Payment>();
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void AddPayment(Payment payment)
    {
        Payments.Add(payment);
    }

    public Result UpdateToStatus(OrderStatus toStatus)
    {
        if (Status == toStatus)
        {
            return Result.Fail(new OrderAlreadyWithStatusError(toStatus));
        }

        switch (Status, toStatus)
        {
            case (OrderStatus.None, OrderStatus.Created):
                Status = OrderStatus.Created;
                break;
            case (OrderStatus.Created, OrderStatus.Received):
                Status = OrderStatus.Received;
                break;
            case (OrderStatus.Received, OrderStatus.OnGoing):
                Status = OrderStatus.OnGoing;
                break;
            case (OrderStatus.OnGoing, OrderStatus.Done):
                Status = OrderStatus.Done;
                break;
            case (OrderStatus.Done, OrderStatus.Completed):
                Status = OrderStatus.Completed;
                break;
            default:
                return Result.Fail(new OrderInvalidStatusTransitionError(Status, toStatus));
        }

        StatusUpdatedAt = DateTime.UtcNow;

        return Result.Ok();
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
