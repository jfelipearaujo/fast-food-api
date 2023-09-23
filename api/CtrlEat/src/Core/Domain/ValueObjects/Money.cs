using Domain.Errors.ValueObjects.Money;

using FluentResults;

namespace Domain.ValueObjects
{
    public class Money
    {
        private const int MAX_LENGTH_CURRENCY = 3;

        public string Currency { get; private set; }

        public decimal Amount { get; private set; }

        private Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public static Result<Money> Create(string currency, decimal amount)
        {
            if (string.IsNullOrEmpty(currency) || currency.Length > MAX_LENGTH_CURRENCY)
            {
                return Result.Fail(new MoneyInvalidCurrencyError());
            }

            if (amount <= 0)
            {
                return Result.Fail(new MoneyInvalidAmountError());
            }

            return new Money(currency, amount);
        }
    }
}
