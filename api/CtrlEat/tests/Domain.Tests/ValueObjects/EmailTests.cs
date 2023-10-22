using Domain.Entities.ClientAggregate.Errors;
using Domain.Entities.ClientAggregate.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void ShouldCreateEmailWithValidAddress()
    {
        // Arrange
        var email = "email@test.com";

        var expected = Email.Create(email);

        // Act
        var result = Email.Create(email);

        // Assert
        result.Should().BeSuccess();
        result.Value.Equals(expected.Value).Should().BeTrue();
    }

    [Fact]
    public void ShouldCreateAnEmptyEmailWhenNoAddressIsProvided()
    {
        // Arrange
        var email = "";

        var expected = Email.Create("");

        // Act
        var result = Email.Create(email);

        // Assert
        result.Should().BeSuccess();
        result.Value.Equals(expected.Value).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotCreateEmailWithInvalidAddress()
    {
        // Arrange
        var email = "email";

        // Act
        var result = Email.Create(email);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new EmailInvalidAddressError());
    }
}
