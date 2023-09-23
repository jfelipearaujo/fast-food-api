namespace Domain.Entities.StrongIds
{
    public class ProductCategoryId
    {
        public Guid Value { get; private set; }

        private ProductCategoryId(Guid value)
        {
            Value = value;
        }

        public static ProductCategoryId Create(Guid value)
        {
            return new ProductCategoryId(value);
        }
    }
}
