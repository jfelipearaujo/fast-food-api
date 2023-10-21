using Domain.Entities.ClientAggregate;

namespace Domain.Tests.Entities;

public class ClientTests
{
    [Fact]
    public void ShouldCreateValidClient()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var emailAddress = "john.doe@email.com";
        var documentId = "34430703027";

        // Act
        var result = Client.Create(firstName, lastName, emailAddress, documentId);

        // Assert
        result.Should().BeSuccess();
    }

    [Theory]
    [InlineData("", "Doe", "john.doe@email.com", "34430703027")]
    [InlineData("John", "Doe", "john.doe@email", "34430703027")]
    [InlineData("John", "Doe", "john.doe@email.com", "123456")]
    public void ShouldNotCreateClienteWhenSomeDataIsInvalid(
        string firstName,
        string lastName,
        string emailAddress,
        string documentId)
    {
        // Arrange

        // Act
        var result = Client.Create(firstName, lastName, emailAddress, documentId);

        // Assert
        result.Should().BeFailure();
    }
}
