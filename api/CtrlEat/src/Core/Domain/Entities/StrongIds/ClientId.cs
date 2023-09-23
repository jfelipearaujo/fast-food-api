namespace Domain.Entities.StrongIds
{
    public class ClientId
    {
        public Guid Value { get; private set; }

        private ClientId(Guid value)
        {
            Value = value;
        }

        public static ClientId Create(Guid value)
        {
            return new ClientId(value);
        }
    }
}
