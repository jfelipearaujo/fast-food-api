namespace Domain.Entities.TypedIds
{
    public class ClientId
    {
        public Guid Value { get; private set; }

        public ClientId(Guid value)
        {
            Value = value;
        }
    }
}
