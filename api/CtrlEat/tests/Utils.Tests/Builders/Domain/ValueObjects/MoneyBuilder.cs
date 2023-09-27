using Domain.Entities.ProductAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.ValueObjects;

public class MoneyBuilder
{
    private string currency;
    private decimal amount;

    public MoneyBuilder()
    {
        Reset();
    }

    public MoneyBuilder Reset()
    {
        currency = default;
        amount = default;

        return this;
    }

    public MoneyBuilder WithSample()
    {
        currency = "BRL";
        amount = 1.5m;

        return this;
    }

    public MoneyBuilder WithCurrency(string currency)
    {
        this.currency = currency;

        return this;
    }

    public MoneyBuilder WithAmount(decimal amount)
    {
        this.amount = amount;

        return this;
    }

    public Money Build()
    {
        return Money.Create(amount, currency).ValueOrDefault;
    }
}