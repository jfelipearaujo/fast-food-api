using Domain.Abstract;
using Domain.Errors.ProductCategories;

using FluentResults;

namespace Domain.Entities
{
    public class ProductCategory : Entity<Guid>
    {
        private const int MAX_DESCRIPTION_LENGTH = 250;

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

        public static Result<ProductCategory> ValidateAndCreate(
            string description)
        {

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Fail(new ProductCategoryDescriptionInvalidError());
            }

            if (description.Length > MAX_DESCRIPTION_LENGTH)
            {
                return Result.Fail(new ProductCategoryDescriptionMaxLengthError(MAX_DESCRIPTION_LENGTH));
            }

            return new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = description,
            };
        }
    }
}