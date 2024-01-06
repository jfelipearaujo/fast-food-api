namespace Domain.UseCases.Orders.CheckoutHookOrder.Requests;

public class CheckoutHookOrderRequest
{
    public Guid OrderId { get; set; }

    public bool PaymentApproved { get; set; }
}
