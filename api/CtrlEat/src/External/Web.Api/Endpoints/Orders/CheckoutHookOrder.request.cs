namespace Web.Api.Endpoints.Orders;

public class CheckoutHookOrderEndpointRequest
{
    public Guid OrderId { get; set; }

    public bool PaymentApproved { get; set; }
}