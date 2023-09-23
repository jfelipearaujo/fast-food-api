namespace Domain.Adapters.Models
{
    public class ProductModel : IBaseModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public Guid ProductCategoryId { get; set; }

        public virtual ProductCategoryModel ProductCategory { get; set; }
    }
}
