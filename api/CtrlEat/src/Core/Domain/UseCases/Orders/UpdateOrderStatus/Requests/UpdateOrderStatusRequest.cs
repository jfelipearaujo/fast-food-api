namespace Domain.UseCases.Orders.UpdateOrderStatus.Requests;

public class UpdateOrderStatusRequest
{
    public Guid OrderId { get; set; }

    public string Status { get; set; }
}