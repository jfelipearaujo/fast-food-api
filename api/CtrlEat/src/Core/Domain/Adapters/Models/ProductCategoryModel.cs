namespace Domain.Adapters.Models
{
    public class ProductCategoryModel : IBaseModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
