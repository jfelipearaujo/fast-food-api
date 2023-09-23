using Domain.Abstract;
using Domain.Entities.TypedIds;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Product : Entity<ProductId>
    {
        public string Description { get; set; }

        public Money Price { get; set; }

        public string ImageUrl { get; set; }


        public ProductCategoryId ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
