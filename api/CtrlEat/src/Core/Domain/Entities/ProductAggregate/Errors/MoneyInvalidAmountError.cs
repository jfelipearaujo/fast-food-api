using FluentResults;

namespace Domain.Entities.ProductAggregate.Errors;

public class MoneyInvalidAmountError : Error
{
    public MoneyInvalidAmountError()
        : base("O valor do preço unitário não pode ser menor ou igual a zero")
    {
    }
}

public class StockInvalidQuantityError : Error
{
    public StockInvalidQuantityError()
        : base("O valor do estoque do produto deve ser maior ou igual a 1")
    {
    }
}