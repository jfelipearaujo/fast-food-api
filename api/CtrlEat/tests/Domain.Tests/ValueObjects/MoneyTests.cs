using Domain.Entities.ProductAggregate.Errors;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentAssertions;

namespace Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void ShouldCreateValidMoney()
    {
        // Arrange

        // Act
        var result = Money.Create(10.5m, "BRL");

        // Assert
        result.Should().BeSuccess();
        result.Value.Currency.Should().Be("BRL");
        result.Value.Amount.Should().Be(10.5m);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("bigger currency")]
    public void ShouldNotCreateMoneyWithInvalidCurrency(string currency)
    {
        // Arrange

        // Act
        var result = Money.Create(10.5m, currency);

        // Assert
        result.Should().BeFailure().And.HaveReason(new MoneyInvalidCurrencyError());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldNotCreateMoneyWithInvalidAmount(decimal amount)
    {
        // Arrange

        // Act
        var result = Money.Create(amount, "BRL");

        // Assert
        result.Should().BeFailure().And.HaveReason(new MoneyInvalidAmountError());
    }
}
