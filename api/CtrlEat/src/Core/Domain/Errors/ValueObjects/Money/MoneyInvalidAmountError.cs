using FluentResults;

namespace Domain.Errors.ValueObjects.Money
{
    public class MoneyInvalidAmountError : Error
    {
        public MoneyInvalidAmountError()
            : base("O valor da quantidade não pode ser menor ou igual a zero")
        {
        }
    }
}
