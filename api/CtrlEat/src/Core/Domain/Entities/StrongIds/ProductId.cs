namespace Domain.Entities.StrongIds
{
    public class ProductId
    {
        public Guid Value { get; private set; }

        private ProductId(Guid value)
        {
            Value = value;
        }

        public static ProductId Create(Guid value)
        {
            return new ProductId(value);
        }
    }
}
