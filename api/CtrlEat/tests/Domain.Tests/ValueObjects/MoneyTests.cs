using Domain.Entities.ProductAggregate.Errors;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void ShouldCreateValidMoney()
    {
        // Arrange
        var expected = Money.Create(10.5m, Money.BRL);

        // Act
        var result = Money.Create(10.5m, Money.BRL);

        // Assert
        result.Should().BeSuccess();
        result.Value.Currency.Should().Be(Money.BRL);
        result.Value.Amount.Should().Be(10.5m);
        result.Value.Equals(expected.Value).Should().BeTrue();
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
        var result = Money.Create(amount, Money.BRL);

        // Assert
        result.Should().BeFailure().And.HaveReason(new MoneyInvalidAmountError());
    }
}
