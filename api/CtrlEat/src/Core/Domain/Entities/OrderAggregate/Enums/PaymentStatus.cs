namespace Domain.Entities.OrderAggregate.Enums;

public enum PaymentStatus
{
    None = 0,
    WaitingApproval = 1,
    Approved = 2,
    Rejected = 3
}