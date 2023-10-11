using FluentResults;

namespace Domain.Entities.OrderAggregate.Enums;

public enum OrderStatus
{
    None = 0,
    Created = 1,
    Received = 2,
    OnGoing = 3,
    Done = 4,
    Completed = 5
}

public static class OrderStatusStateMachine
{
    public static Result<OrderStatus> MoveTo(OrderStatus from, OrderStatus to)
    {
        switch (from, to)
        {
            case (OrderStatus.None, OrderStatus.Created):
                return OrderStatus.Created;
            case (OrderStatus.Created, OrderStatus.Received):
                return OrderStatus.Received;
            case (OrderStatus.Received, OrderStatus.OnGoing):
                return OrderStatus.OnGoing;
            case (OrderStatus.OnGoing, OrderStatus.Done):
                return OrderStatus.Done;
            case (OrderStatus.Done, OrderStatus.Completed):
                return OrderStatus.Completed;
            default:
                return Result.Fail($"Não é possível transicionar o status de '{from}' para '{to}'");
        }
    }
}
