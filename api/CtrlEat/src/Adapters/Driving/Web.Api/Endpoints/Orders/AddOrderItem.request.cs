namespace Web.Api.Endpoints.Orders;

public class AddOrderItemEndpointRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string? Observation { get; set; }
}