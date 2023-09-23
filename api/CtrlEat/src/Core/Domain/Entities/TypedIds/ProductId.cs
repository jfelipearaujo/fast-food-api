namespace Domain.Entities.TypedIds
{
    public class ProductId
    {
        public Guid Value { get; private set; }

        public ProductId(Guid value)
        {
            Value = value;
        }
    }
}
