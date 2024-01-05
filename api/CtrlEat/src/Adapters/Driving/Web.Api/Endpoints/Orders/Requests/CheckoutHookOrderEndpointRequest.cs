namespace Web.Api.Endpoints.Orders.Requests;

public class CheckoutHookOrderEndpointRequest
{
    public Guid OrderId { get; set; }

    public bool PaymentApproved { get; set; }
}