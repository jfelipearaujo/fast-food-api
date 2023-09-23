using Domain.Entities;

namespace Domain.UseCases.ProductCategories.Responses
{
    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        // ---

        public static ProductCategoryResponse MapFromDomain(ProductCategory productCategory)
        {
            return new ProductCategoryResponse
            {
                Id = productCategory.Id.Value,
                Description = productCategory.Description,
                CreatedAtUtc = productCategory.CreatedAtUtc,
                UpdatedAtUtc = productCategory.UpdatedAtUtc,
            };
        }
    }
}