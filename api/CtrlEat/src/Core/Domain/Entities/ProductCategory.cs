using Domain.Abstract;

namespace Domain.Entities
{
    public class ProductCategory : Entity
    {
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}