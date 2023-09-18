using Domain.Abstract;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string Currency { get; set; }

        public string ImageUrl { get; set; }


        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
