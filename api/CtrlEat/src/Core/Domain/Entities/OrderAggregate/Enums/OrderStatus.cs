namespace Domain.Entities.OrderAggregate.Enums;

public enum OrderStatus
{
    None = 0,
    Created = 1,
    Received = 2,
    OnGoing = 3,
    Done = 4,
    Completed = 5,
    Cancelled = 6
}
