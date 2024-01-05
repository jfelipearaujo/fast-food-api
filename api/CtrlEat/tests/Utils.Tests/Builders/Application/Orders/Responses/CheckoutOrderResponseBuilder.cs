using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.CheckoutOrder.Responses;

namespace Utils.Tests.Builders.Application.Orders.Responses;

public class CheckoutOrderResponseBuilder
{
    private PaymentStatus status;

    public CheckoutOrderResponseBuilder()
    {
        Reset();
    }

    public CheckoutOrderResponseBuilder Reset()
    {
        status = default;

        return this;
    }

    public CheckoutOrderResponseBuilder WithSample()
    {
        status = PaymentStatus.WaitingApproval;
        return this;
    }

    public CheckoutOrderResponseBuilder WithStatus(PaymentStatus status)
    {
        this.status = status;
        return this;
    }

    public CheckoutOrderResponse Build()
    {
        return new CheckoutOrderResponse
        {
            PaymentStatus = status
        };
    }
}
