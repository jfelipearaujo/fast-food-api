using Domain.Entities.OrderAggregate.Enums;

using FluentResults;

namespace Domain.Entities.OrderAggregate.Errors;

public class PaymentAlreadyWithStatusError : Error
{
    public PaymentAlreadyWithStatusError(PaymentStatus to)
        : base($"O pagamento já encontra-se no status solicitado: '{to}'")
    {
    }
}