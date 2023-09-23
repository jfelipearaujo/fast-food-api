using FluentResults;

namespace Domain.Errors.ValueObjects.Money
{
    public class MoneyInvalidCurrencyError : Error
    {
        public MoneyInvalidCurrencyError()
            : base("O valor que representa a moeda não pode estar vazio")
        {
        }
    }
}
