using Domain.Entities.TypedIds;
using Domain.ValueObjects;

using Utils.Tests.Builders.Domain.ValueObjects;

namespace Domain.Entities
{
    public class ProductBuilder
    {
        private ProductId id;
        private string description;
        private Money price;
        private string imageUrl;
        private ProductCategoryId productCategoryId;
        private ProductCategory productCategory;

        public ProductBuilder()
        {
            Reset();
        }

        public ProductBuilder Reset()
        {
            id = default;
            description = default;
            price = default;
            imageUrl = default;
            productCategoryId = default;
            productCategory = default;

            return this;
        }

        public ProductBuilder WithSample()
        {
            id = new ProductId(Guid.NewGuid());
            description = "Product Description";
            price = new MoneyBuilder().WithSample().Build();
            imageUrl = "http://image.com/123123.png";
            productCategoryId = new ProductCategoryId(Guid.NewGuid());
            productCategory = new ProductCategoryBuilder().Build();

            return this;
        }

        public ProductBuilder WithId(Guid id)
        {
            this.id = new ProductId(id);

            return this;
        }

        public ProductBuilder WithProductCategoryId(Guid productCategoryId)
        {
            this.productCategoryId = new ProductCategoryId(productCategoryId);

            return this;
        }

        public ProductBuilder WithProductCategoryId(ProductCategoryId productCategoryId)
        {
            this.productCategoryId = productCategoryId;

            return this;
        }

        public ProductBuilder WithDescription(string description)
        {
            this.description = description;

            return this;
        }

        public ProductBuilder WithPrice(string currency, decimal amount)
        {
            price = new MoneyBuilder()
                .WithCurrency(currency)
                .WithAmount(amount)
                .Build();

            return this;
        }

        public ProductBuilder WithImageUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;

            return this;
        }

        public Product Build()
        {
            return new Product
            {
                Id = id,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                ProductCategoryId = productCategoryId,
                ProductCategory = productCategory,
            };
        }


    }
}