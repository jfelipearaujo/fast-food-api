using Domain.Entities.TypedIds;

using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class ProductCategoryBuilder
    {
        private ProductCategoryId id;
        private string description;
        private ICollection<Product> products;

        public ProductCategoryBuilder()
        {
            Reset();
        }

        public ProductCategoryBuilder Reset()
        {
            id = default;
            description = default;
            products = new Collection<Product>();

            return this;
        }

        public ProductCategoryBuilder WithSample()
        {
            id = new ProductCategoryId(Guid.NewGuid());
            description = "Product Category";

            return this;
        }

        public ProductCategoryBuilder WithId(Guid id)
        {
            this.id = new ProductCategoryId(id);

            return this;
        }

        public ProductCategoryBuilder WithDescription(string description)
        {
            this.description = description;

            return this;
        }

        public ProductCategory Build()
        {
            return new ProductCategory
            {
                Id = id,
                Description = description,
                Products = products,
            };
        }
    }
}