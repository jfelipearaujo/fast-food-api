namespace Domain.Entities.TypedIds
{
    public class ProductCategoryId
    {
        public Guid Value { get; private set; }

        public ProductCategoryId(Guid value)
        {
            Value = value;
        }
    }
}
