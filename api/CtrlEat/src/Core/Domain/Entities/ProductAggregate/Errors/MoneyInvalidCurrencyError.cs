using FluentResults;

namespace Domain.Entities.ProductAggregate.Errors;

public class MoneyInvalidCurrencyError : Error
{
    public MoneyInvalidCurrencyError()
        : base("O valor que representa a moeda não pode estar vazio")
    {
    }
}
