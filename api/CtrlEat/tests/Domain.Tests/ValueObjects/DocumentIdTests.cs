using Domain.Entities.ClientAggregate.Errors;
using Domain.Entities.ClientAggregate.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class DocumentIdTests
{
    [Fact]
    public void ShouldCreateValidDocumentId()
    {
        // Arrange
        var documentId = "01629925055";

        var expected = DocumentId.Create(documentId);

        // Act
        var result = DocumentId.Create(documentId);

        // Assert
        result.Should().BeSuccess();
        result.Value.Value.Should().Be(documentId);
        result.Value.Equals(expected.Value).Should().BeTrue();
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    public void ShouldCreateValidEmptyDocumentId(string documentId, string expected)
    {
        // Arrange

        // Act
        var result = DocumentId.Create(documentId);

        // Assert
        result.Should().BeSuccess();
        result.Value.Value.Should().Be(expected);
    }

    [Fact]
    public void ShouldNotCreateWithInvalidDocumentId()
    {
        // Arrange
        var documentId = "123456";

        // Act
        var result = DocumentId.Create(documentId);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new InvalidDocumentIdError());
    }
}
