using Domain.Abstract;
using Domain.Entities.StrongIds;

namespace Domain.Entities
{
    public class ProductCategory : Entity<ProductCategoryId>
    {
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}