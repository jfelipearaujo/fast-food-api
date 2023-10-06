namespace Web.Api.Endpoints.Orders.Requests;

public class AddOrderItemEndpointRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string? Observation { get; set; }
}