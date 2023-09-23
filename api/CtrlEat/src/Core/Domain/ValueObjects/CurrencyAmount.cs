using Domain.Abstract;
using Domain.Errors.ValueObjects.Money;

using FluentResults;

namespace Domain.ValueObjects
{
    public class CurrencyAmount : ValueObject, IValueObject<CurrencyAmount>
    {
        private const int MIN_CURRENCY_AMOUNT = 0;

        public decimal Value { get; private set; }

        private CurrencyAmount()
        {
        }

        public CurrencyAmount(decimal amount)
        {
            Value = amount;
        }

        public Result<CurrencyAmount> Validate()
        {
            if (Value <= MIN_CURRENCY_AMOUNT)
            {
                return Result.Fail(new MoneyInvalidAmountError());
            }

            return Result.Ok();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
