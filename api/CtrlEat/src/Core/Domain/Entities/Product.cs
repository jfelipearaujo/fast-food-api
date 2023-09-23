using Domain.Abstract;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Description { get; set; }

        public Currency Currency { get; set; }

        public CurrencyAmount Amount { get; set; }

        public string ImageUrl { get; set; }

        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
