using Domain.Errors.ValueObjects.Money;
using Domain.ValueObjects;

using FluentAssertions;

namespace Domain.Tests.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void ShouldCreateValidMoney()
        {
            // Arrange

            // Act
            var result = Money.Create("BRL", 10.5m);

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
            var result = Money.Create(currency, 10.5m);

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
            var result = Money.Create("BRL", amount);

            // Assert
            result.Should().BeFailure().And.HaveReason(new MoneyInvalidAmountError());
        }
    }
}
