namespace Web.Api.Endpoints.Orders.Responses;

public class OrderItemEndpointResponse
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public string Observation { get; set; }

    public string Description { get; set; }

    public string Price { get; set; }
}