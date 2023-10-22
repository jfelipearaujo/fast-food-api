using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.Errors;

namespace Domain.Tests.Entities;

public class ProductCategoryTests
{
    [Fact]
    public void ShouldCreateProductCategoryCorrectly()
    {
        // Arrange
        var description = "Product Category Description";

        // Act
        var result = ProductCategory.Create(description);

        // Assert
        result.Should().BeSuccess();
    }

    [Fact]
    public void ShouldNotCreateProductCategoryWithInvalidDescription()
    {
        // Arrange
        var description = "";

        // Act
        var result = ProductCategory.Create(description);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new ProductCategoryDescriptionInvalidError());
    }

    [Fact]
    public void ShouldNotCreateProductCategoryWithTooLongDescription()
    {
        // Arrange
        var description = string.Join("", Enumerable.Repeat("a", ProductCategory.MAX_DESCRIPTION_LENGTH + 1));

        // Act
        var result = ProductCategory.Create(description);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new ProductCategoryDescriptionMaxLengthError(ProductCategory.MAX_DESCRIPTION_LENGTH));
    }
}
