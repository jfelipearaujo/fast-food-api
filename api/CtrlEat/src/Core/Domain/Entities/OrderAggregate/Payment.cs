using Domain.Common.Models;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.Errors;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.OrderAggregate;

public sealed class Payment : AggregateRoot<PaymentId>
{
    public OrderId OrderId { get; private set; }

    public Order Order { get; private set; }

    public PaymentStatus Status { get; set; }

    public Money Price { get; private set; }

    [ExcludeFromCodeCoverage]
    private Payment()
    {
    }

    private Payment(
        OrderId orderId,
        PaymentStatus status,
        Money price,
        PaymentId? paymentId = null)
        : base(paymentId ?? PaymentId.CreateUnique())
    {
        OrderId = orderId;
        Status = status;
        Price = price;
    }

    public Result UpdateToStatus(PaymentStatus toStatus)
    {
        if (Status == toStatus)
        {
            return Result.Fail(new PaymentAlreadyWithStatusError(toStatus));
        }

        switch (Status, toStatus)
        {
            case (PaymentStatus.None, PaymentStatus.WaitingApproval):
                Status = PaymentStatus.WaitingApproval;
                break;
            case (PaymentStatus.WaitingApproval, PaymentStatus.Approved):
                Status = PaymentStatus.Approved;
                break;
            case (PaymentStatus.WaitingApproval, PaymentStatus.Rejected):
                Status = PaymentStatus.Rejected;
                break;
            default:
                return Result.Fail(new PaymentInvalidStatusTransitionError(Status, toStatus));
        }

        return Result.Ok();
    }

    public static Result<Payment> Create(
        OrderId orderId,
        Money price,
        PaymentStatus? status = null,
        PaymentId? paymentId = null)
    {
        return new Payment(
            orderId,
            status ?? PaymentStatus.WaitingApproval,
            price,
            paymentId);
    }
}
