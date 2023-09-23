using Domain.Abstract;
using Domain.Errors.ValueObjects.Money;

using FluentResults;

namespace Domain.ValueObjects
{
    public class Currency : ValueObject, IValueObject<Currency>
    {
        private const int MAX_LENGTH_CURRENCY = 3;

        public string Value { get; private set; }

        private Currency()
        {
        }

        public Currency(string currency)
        {
            Value = currency;
        }

        public Result<Currency> Validate()
        {
            if (string.IsNullOrEmpty(Value) || Value.Length > MAX_LENGTH_CURRENCY)
            {
                return Result.Fail(new MoneyInvalidCurrencyError());
            }

            return Result.Ok();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
