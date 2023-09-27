using FluentResults;

namespace Domain.Entities.ProductAggregate.Errors;

public class MoneyInvalidAmountError : Error
{
    public MoneyInvalidAmountError()
        : base("O valor do preço unitário não pode ser menor ou igual a zero")
    {
    }
}
