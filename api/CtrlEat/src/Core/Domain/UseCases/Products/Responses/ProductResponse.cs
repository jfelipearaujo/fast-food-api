using Domain.UseCases.ProductCategories.Responses;

namespace Domain.UseCases.Products.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string Currency { get; set; }

        public string ImageUrl { get; set; }

        public ProductCategoryResponse ProductCategory { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}
