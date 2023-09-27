using Domain.Common.Models;
using Domain.Entities.ProductAggregate.Errors;

using FluentResults;

namespace Domain.Entities.ProductAggregate.ValueObjects;

public sealed class Money : ValueObject
{
    private const int MAX_LENGTH_CURRENCY = 3;
    private const int MIN_CURRENCY_AMOUNT = 0;

    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    private Money()
    {

    }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, string currency)
    {
        if (string.IsNullOrEmpty(currency) || currency.Length > MAX_LENGTH_CURRENCY)
        {
            return Result.Fail(new MoneyInvalidCurrencyError());
        }

        if (amount <= MIN_CURRENCY_AMOUNT)
        {
            return Result.Fail(new MoneyInvalidAmountError());
        }

        return new Money(amount, currency);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
