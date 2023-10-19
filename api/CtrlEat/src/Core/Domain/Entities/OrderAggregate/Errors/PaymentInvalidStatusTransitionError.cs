using Domain.Entities.OrderAggregate.Enums;

using FluentResults;

namespace Domain.Entities.OrderAggregate.Errors;

public class PaymentInvalidStatusTransitionError : Error
{
    public PaymentInvalidStatusTransitionError(PaymentStatus from, PaymentStatus to)
        : base($"Não é possível transicionar do status '{from}' para '{to}'")
    {
    }
}