using Domain.Entities.ClientAggregate.Errors;
using Domain.Entities.ClientAggregate.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class FullNameTests
{
    [Fact]
    public void ShouldCreateValidFullName()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";

        var expected = FullName.Create(firstName, lastName);

        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        result.Should().BeSuccess();
        result.Value.Equals(expected.Value).Should().BeTrue();
    }

    [Fact]
    public void ShouldCreateAnEmptyFullName()
    {
        // Arrange
        var firstName = "";
        var lastName = "";

        var expected = FullName.Create(firstName, lastName);

        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        result.Should().BeSuccess();
        result.Value.Equals(expected.Value).Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnErrorWhenOnlyLastNameIsProvided()
    {
        // Arrange
        var firstName = "";
        var lastName = "Doe";

        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new FullNameMissingFirstNameError());
    }

    [Fact]
    public void ShouldReturnErrorWhenFirstNameExceedMaxLength()
    {
        // Arrange
        var firstName = string.Join("", Enumerable.Repeat("a", FullName.MAX_LENGTH + 1));
        var lastName = "Doe";

        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new FullNameInvalidLengthError(FullName.MAX_LENGTH));
    }

    [Fact]
    public void ShouldReturnErrorWhenLastNameExceedMaxLength()
    {
        // Arrange
        var firstName = "John";
        var lastName = string.Join("", Enumerable.Repeat("a", FullName.MAX_LENGTH + 1));

        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new FullNameInvalidLengthError(FullName.MAX_LENGTH));
    }
}
