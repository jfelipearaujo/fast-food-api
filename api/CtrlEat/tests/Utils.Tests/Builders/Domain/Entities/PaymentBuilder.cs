using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class PaymentBuilder
{
    private Money price;
    private Order order;
    private PaymentStatus status;

    public PaymentBuilder()
    {
        Reset();
    }

    public PaymentBuilder Reset()
    {
        price = default;
        order = default;
        status = default;

        return this;
    }

    public PaymentBuilder WithSample()
    {
        WithPrice(Money.Create(100, Money.BRL).Value);
        WithOrder(new OrderBuilder().WithSample().Build());
        WithStatus(PaymentStatus.WaitingApproval);

        return this;
    }

    public PaymentBuilder WithPrice(Money price)
    {
        this.price = price;

        return this;
    }

    public PaymentBuilder WithOrder(Order order)
    {
        this.order = order;

        return this;
    }

    public PaymentBuilder WithStatus(PaymentStatus status)
    {
        this.status = status;

        return this;
    }

    public Payment Build()
    {
        return Payment.Create(order.Id, price, status).Value;
    }
}
