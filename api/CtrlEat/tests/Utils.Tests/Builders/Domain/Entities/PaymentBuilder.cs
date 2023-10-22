using Domain.Entities.OrderAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class PaymentBuilder
{
    private Money price;
    private Order order;

    public PaymentBuilder()
    {
        Reset();
    }

    public PaymentBuilder Reset()
    {
        price = default;
        order = default;

        return this;
    }

    public PaymentBuilder WithSample()
    {
        price = Money.Create(100, Money.BRL).Value;
        order = new OrderBuilder().WithSample().Build();

        return this;
    }

    public Payment Build()
    {
        return Payment.Create(order.Id, price).Value;
    }
}
