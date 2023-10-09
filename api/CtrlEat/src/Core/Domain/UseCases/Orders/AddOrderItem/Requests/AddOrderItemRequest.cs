namespace Domain.UseCases.Orders.AddOrderItem.Requests;

public class AddOrderItemRequest
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string? Observation { get; set; }
}
