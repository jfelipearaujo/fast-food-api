using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.Errors;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Domain.Tests.Entities;

public class PaymentTests
{
    [Fact]
    public void ShouldCreateValidPayment()
    {
        // Arrange
        var orderId = OrderId.CreateUnique();
        var price = Money.Create(100, Money.BRL).Value;

        // Act
        var result = Payment.Create(orderId, price);

        // Assert
        result.Should().BeSuccess();
    }

    [Theory]
    [InlineData(PaymentStatus.None, PaymentStatus.WaitingApproval, PaymentStatus.WaitingApproval)]
    [InlineData(PaymentStatus.WaitingApproval, PaymentStatus.Approved, PaymentStatus.Approved)]
    [InlineData(PaymentStatus.WaitingApproval, PaymentStatus.Rejected, PaymentStatus.Rejected)]
    public void ShouldUpdatePaymentStatusCorrectly(
        PaymentStatus currentStatus,
        PaymentStatus toStatus,
        PaymentStatus expected)
    {
        // Arrange
        var orderId = OrderId.CreateUnique();
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(orderId, price, currentStatus).Value;

        // Act
        var result = payment.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeSuccess();
        payment.Status.Should().Be(expected);
    }

    [Fact]
    public void ShouldNotUpdatePaymentStatusToTheSameStatus()
    {
        // Arrange
        var currentStatus = PaymentStatus.WaitingApproval;
        var toStatus = PaymentStatus.WaitingApproval;

        var orderId = OrderId.CreateUnique();
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(orderId, price, currentStatus).Value;

        // Act
        var result = payment.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new PaymentAlreadyWithStatusError(toStatus));
    }

    [Fact]
    public void ShouldNotUpdatePaymentStatusToAnInvalid()
    {
        // Arrange
        var currentStatus = PaymentStatus.WaitingApproval;
        var toStatus = PaymentStatus.None;

        var orderId = OrderId.CreateUnique();
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(orderId, price, currentStatus).Value;

        // Act
        var result = payment.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new PaymentInvalidStatusTransitionError(currentStatus, toStatus));
    }
}
